using Core.Entities;

namespace Core.Repositories
{
    public interface ITaskRepository : IBaseRepository<Task>
    {
        System.Threading.Tasks.Task UpdateAsync(int taskId, Task task);
        System.Threading.Tasks.Task AddStandardAnswerAsync(int taskId, string standardAnswer);
    }
}
