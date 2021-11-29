using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface IRequirementManager
    {
        Task<IList<RequirementType>> GetTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
        Task<IList<Requirement>> GetCorrectRequirementsByIds(IList<int> ids);
        Task<IList<Requirement>> GetWrongRequirementsByIds(IList<int> ids);
    }
}
