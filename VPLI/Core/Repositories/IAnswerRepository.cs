using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Repositories
{
    public interface IAnswerRepository : IBaseRepository<UserAnswer>
    {
        Task<ICollection<UserAnswer>> GetByUserAsync(int userId);
        Task<ICollection<UserAnswer>> GetByTaskAsync(int taskId);
    }
}
