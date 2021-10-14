using Core.Entities;
using Core.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly VlpiContext _context;

        public UserRepository(VlpiContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            var isEmailDuplicated = await _context.User.AnyAsync(u => u.Email == user.Email);

            if (isEmailDuplicated)
            {
                throw new Exception($"Email: '{user.Email}' is already registered.");
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.User
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
