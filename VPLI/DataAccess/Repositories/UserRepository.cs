using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VLPIContext _context;

        public UserRepository(VLPIContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            var isEmailDuplicated = await _context.User.AnyAsync(u => u.Email == user.Email);

            if (isEmailDuplicated)
            {
                throw new Exception($"Email: '{user.Email}' is already registered.");
            }

            user.UserRole = new List<UserRole>
            {
                new UserRole
                {
                    RoleId = 1
                }
            };
            user.Password = GetHashedPassword(user.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IList<User>> ListAsync()
        {
            return await _context.User
                .AsNoTracking()
                .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.User
                .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var toBeRemoved = await _context.User.SingleOrDefaultAsync(t => t.Id == id);
            _context.User.Remove(toBeRemoved);
            await _context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var hashedPassword = GetHashedPassword(password);
            var user = await _context.User
                .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u =>
                    u.Email == email && u.Password == hashedPassword);
            return user;
        }

        public async Task AddUserRolesAsync(int userId, string[] roles)
        {
            var user = await _context.User
                .Include(u => u.UserRole)
                .ThenInclude(userRole => userRole.Role)
                .SingleOrDefaultAsync(u => u.Id == userId);

            var newRoles = roles
                .Where(role => !user.UserRole.Select(ur => ur.Role.Name).Contains(role));

            foreach (var newRole in newRoles)
            {
                var roleToAdd = await _context.Role.SingleOrDefaultAsync(role => role.Name == newRole);
                var userRoleToAdd = new UserRole {RoleId = roleToAdd.Id, UserId = userId};

                await _context.UserRole.AddAsync(userRoleToAdd);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRolesAsync(int userId, string[] roles)
        {
            var user = await _context.User
                .Include(u => u.UserRole)
                .ThenInclude(userRole => userRole.Role)
                .SingleOrDefaultAsync(u => u.Id == userId);

            var rolesToRemove = user.UserRole.Where(userRole => roles.Contains(userRole.Role.Name)).ToList();

            _context.UserRole.RemoveRange(rolesToRemove);

            await _context.SaveChangesAsync();
        }

        private string GetHashedPassword(string input)
        {
            using MD5 md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString().ToUpper();
        }
    }
}
