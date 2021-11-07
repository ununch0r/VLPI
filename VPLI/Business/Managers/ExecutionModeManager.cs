using Core.Entities;
using Core.Managers;
using Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ExecutionModeManager :IExecutionModeManager
    {
        private readonly IExecutionModeRepository _executionModeRepository;

        public ExecutionModeManager(IExecutionModeRepository executionModeRepository)
        {
            _executionModeRepository = executionModeRepository;
        }

        public async Task<IList<ExecutionMode>> GetExecutionModesAsync()
        {
            return await _executionModeRepository.GetExecutionModesAsync();
        }
    }
}
