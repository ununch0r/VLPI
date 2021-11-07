using Core.Entities;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Managers
{
    public interface IUserManager
    {
        Task AddAsync(User user);
        Task<User> GetAsync(int id);
        Task<User> AuthenticateUserAsync(string email, string password);
        Task AddUserRolesAsync(int userId, string[] roles);
        Task RemoveUserRolesAsync(int userId, string[] roles);
    }
}
