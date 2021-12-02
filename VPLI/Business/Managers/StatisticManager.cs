using Core.Entities.Custom.Statistic;
using Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var taskResults = await _answerManager.GetAnswersByTaskAsync(taskId);

            var answersCount = taskResults.Count;
            var totalScore = 0;
            var totalTime = 0;

            foreach (var taskResult in taskResults)
            {
                totalScore += taskResult.Score;
                totalTime += taskResult.TimeSpent;
            }

            return new TaskStatistic
            {
                AverageScore = totalScore / (answersCount * 1.0),
                AverageTime = totalTime / answersCount,
                Complexity = task.Complexity,
                Objective = task.Objective,
                TaskId = taskId,
                UserAnswersCount = answersCount
            };
        }

        public async Task<ModuleStatistic> GetStatisticByModuleAsync()
        {
            var taskResults = await _answerManager.GetAllAnswersAsync();
            var totalResult = 0;
            var totalTime = 0;

            foreach (var taskResult in taskResults)
            {
                totalResult += taskResult.Score;
                totalTime += taskResult.TimeSpent;
            }

            return new ModuleStatistic
            {
                AverageScore = totalResult / (taskResults.Count * 1.0),
                AverageTime = totalTime / taskResults.Count,
                UserAnswersCount = taskResults.Count
            };
        }

        public async Task<GenericUserStatistic> GetGenericUserStatisticAsync(int userId)
        {
            var userAnswers = await _answerManager.GetAnswersByUserAsync(userId);

            return new GenericUserStatistic
            {
                Attempts = userAnswers.Count,
                AverageScore = userAnswers.Average(ua => ua.Score),
                AverageTime = (int)userAnswers.Average(ua => ua.TimeSpent),
                PassedAttempts = userAnswers.Count(ua => ua.Score == 100)
            };
        }

        public async Task<ICollection<UserTaskStatistic>> GetUserStatisticAsync(int userId)
        {
            var userAnswers = await _answerManager.GetAnswersByUserAsync(userId);

            return userAnswers.Select(u => new UserTaskStatistic
            {
                AnswerId = u.Id,
                Objective = u.Task.Objective,
                DatePassed = u.ExecutionDate,
                Score = u.Score,
                TaskType = u.Task.Type.Name,
                TimeSpent = u.TimeSpent
            }).ToList();
        }
    }
}
