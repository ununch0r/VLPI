using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Managers
{
    public interface IRequirementManager
    {
        Task<IList<RequirementType>> GetRequirementTypesAsync();
        Task<IList<Requirement>> AddBulk(IList<Requirement> requirements);
    }
}
