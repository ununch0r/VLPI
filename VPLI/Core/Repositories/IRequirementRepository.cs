using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRequirementRepository
    {
        Task<IList<RequirementType>> GetRequirementTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
        Task<Explanation> AddExplanationAsync(Explanation explanation);
    }
}
