using Core.Entities;
using Core.Repositories;
using System;
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

        public Task AddAsync(UserAnswer userAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
