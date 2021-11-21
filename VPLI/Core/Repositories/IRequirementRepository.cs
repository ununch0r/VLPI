using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = Core.Entities.Task;

namespace Core.Repositories
{
    public interface IRequirementRepository
    {
        Task<IList<RequirementType>> GetRequirementTypesAsync();
        Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements);
    }
}
