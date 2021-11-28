using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface IRequirementManager
    {
        Task<IList<RequirementType>> GetRequirementTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
        Task<Explanation> AddExplanationAsync(Explanation explanation);
    }
}
