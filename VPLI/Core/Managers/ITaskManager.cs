using System.Collections.Generic;
using Core.Entities;

namespace Core.Managers
{
    public interface ITaskManager
    {
        System.Threading.Tasks.Task AddAsync(Task task);
        System.Threading.Tasks.Task<Task> GetAsync(int id);
        System.Threading.Tasks.Task<IList<Task>> ListAsync();
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
