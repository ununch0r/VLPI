using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Custom.Task;

namespace Core.Managers
{
    public interface ITaskManager
    {
        System.Threading.Tasks.Task AddAsync(Task task);
        System.Threading.Tasks.Task UpdateAsync(int taskId, Task task);
        System.Threading.Tasks.Task<Task> GetAsync(int id);
        System.Threading.Tasks.Task<IList<TaskCustomModel>> ListAsync();
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
