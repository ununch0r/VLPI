using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = Core.Entities.Task;

namespace DataAccess.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly VLPIContext _context;

        public RequirementRepository(VLPIContext context)
        {
            _context = context;
        }

        public async Task<IList<RequirementType>> GetRequirementTypesAsync()
        {
            return await _context.RequirementType.ToListAsync();
        }

        public async Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements)
        {
            await _context.Requirement.AddRangeAsync(requirements);
            await _context.SaveChangesAsync();
            return requirements;
        }
    }
}
