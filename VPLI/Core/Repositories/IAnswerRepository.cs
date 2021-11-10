using Core.Entities;
using Task = System.Threading.Tasks.Task;

namespace Core.Repositories
{
    public interface IAnswerRepository
    {
        Task AddAsync(UserAnswer userAnswer);
    }
}
