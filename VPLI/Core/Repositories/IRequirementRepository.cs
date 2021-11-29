using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRequirementRepository : IBaseRepository<Requirement>
    {
        Task<IList<RequirementType>> GetAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
        Task<Explanation> AddAsync(Explanation explanation);
    }
}
