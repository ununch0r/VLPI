using System.Collections.Generic;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IExecutionModeRepository
    {
        Task<IList<ExecutionMode>> GetExecutionModesAsync();
    }
}
