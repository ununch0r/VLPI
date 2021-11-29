using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRequirementRepository : IBaseRepository<Requirement>
    {
        Task<IList<RequirementType>> GetTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
        Task<Explanation> AddExplanationAsync(Explanation explanation);
    }
}
