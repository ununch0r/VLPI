using System.Collections.Generic;
using Core.Entities;
using Core.Managers;
using Core.Repositories;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Business.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IList<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            return await _userRepository.AuthenticateUserAsync(email, password);
        }

        public async Task AddUserRolesAsync(int userId, string[] roles)
        {
            await _userRepository.AddUserRolesAsync(userId, roles);
        }

        public async Task RemoveUserRolesAsync(int userId, string[] roles)
        {
            await _userRepository.RemoveUserRolesAsync(userId, roles);
        }
    }
}
