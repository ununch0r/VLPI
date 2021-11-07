using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface IExecutionModeManager
    {
        Task<IList<ExecutionMode>> GetExecutionModesAsync();
    }
}
