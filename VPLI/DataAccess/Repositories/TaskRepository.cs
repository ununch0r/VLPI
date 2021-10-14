using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly VlpiContext _context;

        public TaskRepository(VlpiContext context)
        {
            _context = context;
        }


        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _context.Task.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<Task> GetAsync(int id)
        {
            return await _context.Task
                .Include(t=>t.Requirement)
                .Include(t=> t.TaskTip)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async System.Threading.Tasks.Task<IList<Task>> ListAsync()
        {
            return await _context.Task
                .Include(t => t.Requirement)
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
    }
}
