using System.Collections.Generic;
using Core.Managers;
using Core.Repositories;
using Core.Entities;

namespace Business.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async System.Threading.Tasks.Task<Task> GetAsync(int id)
        {
            return await _taskRepository.GetAsync(id);
        }

        public async System.Threading.Tasks.Task<IList<Task>> ListAsync()
        {
            return await _taskRepository.ListAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
