using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Managers;
using Core.Repositories;

namespace Business.Managers
{
    public class RequirementManager : IRequirementManager
    {
        private readonly IRequirementRepository _requirementRepository;

        public RequirementManager(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public async Task<IList<RequirementType>> GetTypesAsync()
        {
            return await _requirementRepository.GetTypesAsync();
        }

        public async Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements)
        {
            return await _requirementRepository.AddBulkAsync(requirements);
        }
    }
}
