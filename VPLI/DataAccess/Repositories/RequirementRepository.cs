using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly VLPIContext _context;

        public RequirementRepository(VLPIContext context)
        {
            _context = context;
        }

        public async Task<IList<RequirementType>> GetTypesAsync()
        {
            return await _context.RequirementType.ToListAsync();
        }

        public async Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements)
        {
            await _context.Requirement.AddRangeAsync(requirements);
            await _context.SaveChangesAsync();
            return requirements;
        }

        public async Task<Explanation> AddExplanationAsync(Explanation explanation)
        {
            await _context.Explanation.AddAsync(explanation);
            await _context.SaveChangesAsync();
            return explanation;
        }

        public async Task<IList<Requirement>> GetCorrectRequirementsByIds(IList<int> ids)
        {
            return await _context.Requirement
                .AsNoTracking()
                .Where(req => ids.Contains(req.Id))
                .ToListAsync();
        }

        public async Task<IList<Requirement>> GetWrongRequirementsByIds(IList<int> ids)
        {
            return await _context.Requirement
                .Include(req => req.Explanation)
                .AsNoTracking()
                .Where(req => ids.Contains(req.Id))
                .ToListAsync();
        }

        public async Task<Requirement> AddAsync(Requirement model)
        {
            await _context.Requirement.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IList<Requirement>> ListAsync()
        {
            return  await _context.Requirement.AsNoTracking().ToListAsync();
        }

        public async Task<Requirement> GetAsync(int id)
        {
            return await _context.Requirement.AsNoTracking().SingleOrDefaultAsync(req => req.Id == id);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var toBeRemoved = await _context.Requirement.SingleOrDefaultAsync(t => t.Id == id);
            _context.Requirement.Remove(toBeRemoved);
            await _context.SaveChangesAsync();
        }
    }
}
