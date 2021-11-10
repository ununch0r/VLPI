using Core.Entities.Custom.Statistic;
using Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class StatisticManager : IStatisticManager
    {
        private readonly ITaskManager _taskManager;

        public StatisticManager(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public async Task<TaskStatistic> GetStatisticByTaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TaskStatistic>> GetStatisticByModuleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
