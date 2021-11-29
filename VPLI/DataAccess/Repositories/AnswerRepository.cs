using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly VLPIContext _context;

        public AnswerRepository(VLPIContext context)
        {
            _context = context;
        }

        public async Task<IList<UserAnswer>> ListAsync()
        {
            return await _context.UserAnswer.AsNoTracking().ToListAsync();
        }

        public async Task<UserAnswer> GetAsync(int id)
        {
            return await _context.UserAnswer.AsNoTracking().SingleOrDefaultAsync(answer => answer.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var toBeRemoved = await _context.UserAnswer.SingleOrDefaultAsync(t => t.Id == id);
            _context.UserAnswer.Remove(toBeRemoved);
            await _context.SaveChangesAsync();
        }

        public async Task<UserAnswer> AddAsync(UserAnswer model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
