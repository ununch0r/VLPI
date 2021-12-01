using Core.Entities.Custom.Statistic;
using Core.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class StatisticManager : IStatisticManager
    {
        private readonly IAnswerManager _answerManager;
        private readonly ITaskManager _taskManager;

        public StatisticManager(IAnswerManager answerManager, ITaskManager taskManager)
        {
            _answerManager = answerManager;
            _taskManager = taskManager;
        }

        public async Task<TaskStatistic> GetStatisticByTaskAsync(int taskId)
        {
            var task = await _taskManager.GetAsync(taskId);
            if (task != null)
            {
                var taskResults = await _answerManager.GetUserAnswers(userId: null,taskId: taskId);
                if (taskResults == null || taskResults.Count==0)
                {
                    return null;
                }
                int answersCount = taskResults.Count;
                var averageScore = 0;
                var averageTime = 0;
                var averageComplexity = task.Complexity;

                foreach (var taskResult in taskResults)
                {
                    averageScore+=taskResult.Score;
                    averageTime += taskResult.TimeSpent;

                }

                return new TaskStatistic 
                {
                    AverageScore = averageScore/answersCount,
                    AverageTime = averageTime/answersCount,
                    Complexity = averageComplexity,
                    Objective = "some objective",
                    TaskId = taskId,
                    UserAnswersCount = answersCount
                };
            } else 
            {
                return null;
            }
            
            //throw new NotImplementedException();
        }

        public async Task<ModuleStatistic> GetStatisticByModuleAsync()
        {
                var taskResults = await _answerManager.GetAllAnswersAsync();
                if (taskResults == null || taskResults.Count == 0)
                {
                    return null;
                }
                int answersCount = taskResults.Count;
                var averageScore = 0;
                var averageTime = 0;

                foreach (var taskResult in taskResults)
                {
                    averageScore += taskResult.Score;
                    averageTime += taskResult.TimeSpent;
                }

                return new ModuleStatistic
                {
                    AverageScore = averageScore / answersCount,
                    AverageTime = averageTime / answersCount,
                    UserAnswersCount = answersCount
                };
        }

        public Task<GenericUserStatistic> GetGenericUserStatisticAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserTaskStatistic>> GetUserStatisticAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
