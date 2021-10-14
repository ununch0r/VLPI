using Core.Entities;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(int id);
        //TODO: LogIn, change password
    }
}
