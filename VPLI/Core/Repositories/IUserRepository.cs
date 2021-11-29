using Core.Entities;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task AddUserRolesAsync(int userId, string[] roles);
        Task RemoveUserRolesAsync(int userId, string[] roles);
    }
}
