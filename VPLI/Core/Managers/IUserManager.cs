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
    }
}
