using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> AddAsync(T model);
        Task<IList<T>> ListAsync();
        Task<T> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
