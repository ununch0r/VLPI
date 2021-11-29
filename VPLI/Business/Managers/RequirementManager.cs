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

        public async Task<IList<RequirementType>> GetRequirementTypesAsync()
        {
            return await _requirementRepository.GetAsync();
        }

        public async Task<IList<Requirement>> AddBulkAsync(IList<Requirement> requirements)
        {
            return await _requirementRepository.AddBulkAsync(requirements);
        }

        public async Task<Explanation> AddExplanationAsync(Explanation explanation)
        {
            return await _requirementRepository.AddAsync(explanation);
        }
    }
}
