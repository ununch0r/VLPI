using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.AnswerTemplates;
using Core.Entities.Custom.Task;
using Core.Managers;
using Core.Repositories;
using Newtonsoft.Json;
using Task = Core.Entities.Task;

namespace Business.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ITaskManager _taskManager;

        public AnswerManager(IAnswerRepository answerRepository, ITaskManager taskManager)
        {
            _answerRepository = answerRepository;
            _taskManager = taskManager;
        }

        public Task<WritingTaskResult> VerifyWritingAnswerAsync(int userId, WritingAnswer writingAnswer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AnalysisTaskResult> VerifyAndSaveAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer)
        {
            var score = await CalculateScoreAsync(analysisAnswer);

            var userAnswer = CreateUserAnswer(userId, analysisAnswer, score);
            await _answerRepository.AddAsync(userAnswer);

            return new AnalysisTaskResult
            {
                Score = score
            };
        }

        private static UserAnswer CreateUserAnswer(int userId, AnalysisAnswer analysisAnswer, int score)
        {
            var analysisAnswerTemplate = new RequirementsAnalysisTaskTemplateAnswer
            {
                CorrectRequirementIds = analysisAnswer.CorrectRequirements,
                WrongRequirements = analysisAnswer.WrongRequirements
            };

            var userAnswer = new UserAnswer
            {
                UserId = userId,
                ExecutionDate = DateTime.Now,
                Score = (byte)score,
                TaskId = analysisAnswer.TaskId,
                Answer = JsonConvert.SerializeObject(analysisAnswerTemplate),
                TimeSpent = (short)analysisAnswer.TimeSpent
            };
            return userAnswer;
        }

        private async Task<int> CalculateScoreAsync(AnalysisAnswer analysisAnswer)
        {
            var lostPointsIndex = 0.0;
            var task = await _taskManager.GetAnalysisTaskAsync(analysisAnswer.TaskId);

            lostPointsIndex += CalculateLostPointsByCorrectRequirements(analysisAnswer, task.StandardAnswer);
            lostPointsIndex += CalculateLostPointsByWrongRequirements(analysisAnswer, task.StandardAnswer);

            var pointsPercentage = lostPointsIndex / (task.StandardAnswer.CorrectRequirementIds.Count() +
                                                      analysisAnswer.ExpectedWrongRequirementsCount);

            var score = 100 - (int)(pointsPercentage * 100);

            var lostPointsByUsingTips = (3 * score * analysisAnswer.UsedTipsCount) / 100.0;
            score -= (int)lostPointsByUsingTips;
            return score;
        }

        private static double CalculateLostPointsByWrongRequirements(AnalysisAnswer analysisAnswer,
            RequirementsAnalysisTaskTemplateAnswer templateAnswer)
        {
            var properWrongRequirements = analysisAnswer.WrongRequirements.Where(req =>
                templateAnswer.WrongRequirements.Any(standartReq => standartReq.RequirementId == req.RequirementId)).ToList();

            var mistakenCorrectRequirementsCount =
                analysisAnswer.ExpectedWrongRequirementsCount - properWrongRequirements.Count;

            var mistakenExplanationCount = 0;

            foreach (var requirement in properWrongRequirements)
            {
                var standartRequirement =
                    templateAnswer.WrongRequirements.First(req => req.RequirementId == requirement.RequirementId);

                if (standartRequirement.ExplanationId != requirement.ExplanationId)
                {
                    mistakenExplanationCount++;
                }
            }

            return mistakenCorrectRequirementsCount + (mistakenExplanationCount / 2.0);
        }

        private static int CalculateLostPointsByCorrectRequirements(AnalysisAnswer analysisAnswer,
            RequirementsAnalysisTaskTemplateAnswer templateAnswer)
        {
            var properCorrectRequirementsCount = analysisAnswer.CorrectRequirements.Count(answerId =>
                templateAnswer.CorrectRequirementIds.Any(standartId => standartId == answerId));

            var mistakenCorrectRequirementsCount =
                templateAnswer.CorrectRequirementIds.Count() - properCorrectRequirementsCount;

            return mistakenCorrectRequirementsCount;
        }
    }
}
