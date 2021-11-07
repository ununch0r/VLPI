using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly VlpiContext _context;

        public RequirementRepository(VlpiContext context)
        {
            _context = context;
        }

        public async Task<IList<RequirementType>> GetRequirementTypesAsync()
        {
            return await _context.RequirementType.ToListAsync();
        }
    }
}
