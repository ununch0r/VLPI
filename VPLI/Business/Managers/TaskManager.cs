using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Managers;
using Core.Repositories;
using Core.Entities;
using Core.Entities.Custom.Task;

namespace Business.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskManager(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async System.Threading.Tasks.Task UpdateAsync(int taskId, Task task)
        {
            foreach (var requirement in task.Requirement)
            {
                requirement.TaskId = taskId;
            }

            foreach (var taskTip in task.TaskTip)
            {
                taskTip.TaskId = taskId;
            }

            await _taskRepository.UpdateAsync(taskId, task);
        }

        public async System.Threading.Tasks.Task<Task> GetAsync(int id)
        {
            return await _taskRepository.GetAsync(id);
        }

        public async System.Threading.Tasks.Task<IList<TaskCustomModel>> ListAsync()
        {
            var tasks =  await _taskRepository.ListAsync();
            var customTasks = _mapper.Map<IList<TaskCustomModel>>(tasks).OrderBy(task => task.Complexity).ToList();
            var order = 1;
            foreach (var taskViewModel in customTasks)
            {
                taskViewModel.Order = order;
                order++;
            }

            return customTasks;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
