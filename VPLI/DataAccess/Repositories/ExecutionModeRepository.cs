using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ExecutionModeRepository : IExecutionModeRepository
    {
        private readonly VLPIContext _context;

        public ExecutionModeRepository(VLPIContext context)
        {
            _context = context;
        }

        public async Task<IList<ExecutionMode>> ListAsync()
        {
            return  await _context.ExecutionMode.ToListAsync();
        }
    }
}
