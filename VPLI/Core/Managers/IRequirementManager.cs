using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface IRequirementManager
    {
        Task<IList<RequirementType>> GetTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
    }
}
