﻿using Core.Entities;
using Core.Repositories;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
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

            user.HashedPasswrod = GetHashedPassword(user.HashedPasswrod);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.User
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.User
                .Include(u => u.UserRole)
                .SingleOrDefaultAsync(u =>
                    u.Email == email && u.HashedPasswrod== GetHashedPassword(password));
            return user;
        }

        private string GetHashedPassword(string passwordHash)
        {
            var data = Encoding.ASCII.GetBytes(passwordHash);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Encoding.ASCII.GetString(md5data);
            return hashedPassword;
        }
    }
}