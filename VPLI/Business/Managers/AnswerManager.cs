using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.AnswerTemplates;
using Core.Managers;
using Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Custom.Task;
using WrittenRequirementTemplateAnswer = Core.Entities.Custom.AnswerResult.WrittenRequirementTemplateAnswer;

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

        public async Task<WritingTaskResult> VerifyWritingAnswerAsync(int userId, WritingAnswer writingAnswer)
        {
            var task = await _taskManager.GetWritingTaskAsync(writingAnswer.TaskId);

            var score = CalculateWritingScore(writingAnswer, task);

            var userAnswer = CreateUserWritingAnswer(userId, writingAnswer, score);
            await _answerRepository.AddAsync(userAnswer);

            return CreateWritingResult(writingAnswer, task, score);
        }

        private int CalculateWritingScore(WritingAnswer answer, WritingTask task)
        {
            var lostPointsIndex = 0.0;

            var possibleSystemNames = task.StandardAnswer.AcceptableSystemNames.Select(name => name.ToLowerInvariant());
            if (!possibleSystemNames.Contains(answer.SystemName.ToLowerInvariant()))
            {
                lostPointsIndex += 2;
            }

            foreach (var answerRequirement in answer.Requirements)
            {
                var templateRequirement = task.Requirement.First(req => req.Id == answerRequirement.RequirementId);

                if (!string.Equals(templateRequirement.Continuation.Content,
                    answerRequirement.Continuation, StringComparison.InvariantCultureIgnoreCase))
                {
                    lostPointsIndex += 0.5;
                }

                if (answerRequirement.TypeId != templateRequirement.TypeId)
                {
                    lostPointsIndex += 0.5;
                }
            }

            var pointsPercentage = lostPointsIndex / (answer.Requirements.Count + 2);

            var score = 100 - (int)(pointsPercentage * 100);

            var lostPointsByUsingTips = (3 * score * answer.UsedTipsCount) / 100.0;
            score -= (int)lostPointsByUsingTips;

            return score < 0 ? 0 : score;
        }

        private UserAnswer CreateUserWritingAnswer(int userId, WritingAnswer answer, int score)
        {
            var analysisAnswerTemplate = new WritingRequirementsAnswer()
            {
                SystemName = answer.SystemName,
                TemplateAnswers = answer.Requirements.Select(req => new WrittenRequirementAnswer{
                    RequirementId = req.RequirementId,
                    RequirementTypeId = req.TypeId,
                    Continuation = req.Continuation
                })
            };

            var userAnswer = new UserAnswer
            {
                UserId = userId,
                ExecutionDate = DateTime.Now,
                Score = (byte)score,
                TaskId = answer.TaskId,
                Answer = JsonConvert.SerializeObject(analysisAnswerTemplate),
                TimeSpent = (short)answer.TimeSpent
            };
            return userAnswer;
        }

        private WritingTaskResult CreateWritingResult(WritingAnswer answer, WritingTask task, int score)
        {
            return new WritingTaskResult
            {
                Score = score,
                TimeSpent = answer.TimeSpent,
                Requirements = task.Requirement.Select(req => new WrittenRequirementTemplateAnswer
                {
                    RequirementStatement = req.Description + " " + req.Continuation.Content.ToLowerInvariant(),
                    RequirementType = req.Type.Name
                }).ToList()
            };
        }

        public async Task<AnalysisTaskResult> VerifyAndSaveAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer)
        {
            var task = await _taskManager.GetAnalysisTaskAsync(analysisAnswer.TaskId);

            var score = CalculateAnalysisScore(analysisAnswer, task);

            var userAnswer = CreateUserAnalysisAnswer(userId, analysisAnswer, score);
            await _answerRepository.AddAsync(userAnswer);

            return CreateAnalysisResultModel(analysisAnswer, task, score);
        }

        public async Task<ICollection<UserAnswer>> GetAnswersByUserAsync(int userId)
        {
            return await _answerRepository.GetByUserAsync(userId);
        }

        public async Task<ICollection<UserAnswer>> GetAnswersByTaskAsync(int taskId)
        {
            return await _answerRepository.GetByTaskAsync(taskId);
        }

        public async Task<ICollection<UserAnswer>> GetAllAnswersAsync()
        {
            return  await _answerRepository.ListAsync();
        }

        private static AnalysisTaskResult CreateAnalysisResultModel(AnalysisAnswer analysisAnswer, AnalysisTask task, int score)
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

        private static UserAnswer CreateUserAnalysisAnswer(int userId, AnalysisAnswer analysisAnswer, int score)
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

        private int CalculateAnalysisScore(AnalysisAnswer analysisAnswer, AnalysisTask task)
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
