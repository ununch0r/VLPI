using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly VLPIContext _context;

        public TaskRepository(VLPIContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<Task> AddAsync(Task task)
        {
            await _context.Task.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task UpdateAsync(int taskId, Task task)
        {
            var taskToUpdate = await _context.Task.SingleOrDefaultAsync(t => t.Id == taskId);

            taskToUpdate.Complexity = task.Complexity;
            taskToUpdate.Objective = task.Objective;

            var tipsToRemove = await _context.TaskTip.Where(taskTip => taskTip.TaskId == taskId).ToListAsync();
            _context.TaskTip.RemoveRange(tipsToRemove);
            await _context.TaskTip.AddRangeAsync(task.TaskTip);

            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<Task> GetAsync(int id)
        {
            return await _context.Task
                .Include(t=>t.Requirement)
                .ThenInclude(req => req.Explanation)
                .Include(t => t.Requirement)
                .ThenInclude(req => req.Continuation)
                .Include(t => t.Requirement)
                .ThenInclude(req => req.Type)
                .Include(t=> t.TaskTip)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async System.Threading.Tasks.Task<IList<Task>> ListAsync()
        {
            return await _context.Task
                .Include(t => t.Type)
                .Include(t => t.Requirement)
                .ThenInclude(req => req.Explanation)
                .Include(t => t.TaskTip)
                .AsNoTracking()
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var taskToBeRemoved = await _context.Task.SingleOrDefaultAsync(t => t.Id == id);
            _context.Task.Remove(taskToBeRemoved);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddStandardAnswerAsync(int taskId, string standardAnswer)
        {
            var taskToUpdate = await _context.Task.SingleOrDefaultAsync(t => t.Id == taskId);
            taskToUpdate.StandardAnswer = standardAnswer;
            await _context.SaveChangesAsync();
        }
    }
}
