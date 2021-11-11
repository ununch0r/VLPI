using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IList<RequirementType>> GetRequirementTypesAsync()
        {
            return await _context.RequirementType.ToListAsync();
        }
    }
}
