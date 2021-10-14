using Core.Entities;
using System.Collections.Generic;

namespace Core.Repositories
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task AddAsync(Task task);
        System.Threading.Tasks.Task<Task> GetAsync(int id);
        System.Threading.Tasks.Task<IList<Task>> ListAsync();
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
