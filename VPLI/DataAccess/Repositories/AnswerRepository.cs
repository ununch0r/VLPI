using Core.Entities;
using Core.Repositories;
using System;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly VlpiContext _context;

        public AnswerRepository(VlpiContext context)
        {
            _context = context;
        }

        public Task AddAsync(UserAnswer userAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
