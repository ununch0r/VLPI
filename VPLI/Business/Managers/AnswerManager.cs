using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.AnswerTemplates;
using Core.Managers;
using Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Custom.Task;

namespace Business.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ITaskManager _taskManager;
        private readonly IRequirementManager _requirementManager;

        public AnswerManager(IAnswerRepository answerRepository, ITaskManager taskManager, IRequirementManager requirementManager)
        {
            _answerRepository = answerRepository;
            _taskManager = taskManager;
            _requirementManager = requirementManager;
        }

        public Task<WritingTaskResult> VerifyWritingAnswerAsync(int userId, WritingAnswer writingAnswer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AnalysisTaskResult> VerifyAndSaveAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer)
        {
            var task = await _taskManager.GetAnalysisTaskAsync(analysisAnswer.TaskId);

            var score = CalculateScore(analysisAnswer, task);

            var userAnswer = CreateUserAnswer(userId, analysisAnswer, score);
            await _answerRepository.AddAsync(userAnswer);

            return CreateResultModel(analysisAnswer, task, score);
        }

        private static AnalysisTaskResult CreateResultModel(AnalysisAnswer analysisAnswer, AnalysisTask task, int score)
        {
            var correctRequirementDescriptions = task.Requirement
                .Where(req => req.IsCorrect)
                .Select(req => req.Description)
                .ToList();

            var wrongRequirementDisplayModels = task.Requirement
                .Where(req => !req.IsCorrect)
                .Select(req => new WrongRequirementDisplay
                {
                    Description = req.Description,
                    Explanation = req.Explanation.Content
                })
                .ToList();

            return new AnalysisTaskResult
            {
                Score = score,
                TimeSpent = analysisAnswer.TimeSpent,
                CorrectRequirements = correctRequirementDescriptions,
                WrongRequirements = wrongRequirementDisplayModels
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

        private int CalculateScore(AnalysisAnswer analysisAnswer, AnalysisTask task)
        {
            var lostPointsIndex = 0.0;

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
