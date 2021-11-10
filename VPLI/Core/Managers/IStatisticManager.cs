using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Custom.Statistic;

namespace Core.Managers
{
    public interface IStatisticManager
    {
        Task<TaskStatistic> GetStatisticByTaskAsync(int taskId);
        Task<ICollection<TaskStatistic>> GetStatisticByModuleAsync();
    }
}
