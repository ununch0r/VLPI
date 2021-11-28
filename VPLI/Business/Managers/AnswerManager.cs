using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.Task;
using Core.Managers;
using Core.Repositories;

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
            try
            {
                var lostPointsIndex = 0.0;
                var task = await _taskManager.GetAnalysisTaskAsync(analysisAnswer.TaskId);

                lostPointsIndex += CalculateLostPointsByCorrectRequirements(analysisAnswer, task);
                lostPointsIndex += CalculateLostPointsByWrongRequirements(analysisAnswer, task);

                var pointsPercentage = lostPointsIndex / (task.StandardAnswer.CorrectRequirementIds.Count() +
                                       analysisAnswer.ExpectedWrongRequirementsCount);

                var score = 100 - (int) (pointsPercentage * 100);

                var lostPointsByUsingTips = (3 * score * analysisAnswer.UsedTipsCount) / 100.0;
                score -= (int)lostPointsByUsingTips;

                return new AnalysisTaskResult
                {
                    Score = score
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static double CalculateLostPointsByWrongRequirements(AnalysisAnswer analysisAnswer,
            TaskWithAnalysisStandartAnswer task)
        {
            var properWrongRequirements = analysisAnswer.WrongRequirements.Where(req =>
                task.StandardAnswer.WrongRequirements.Any(standartReq => standartReq.Id == req.RequirementId)).ToList();

            var mistakenCorrectRequirementsCount =
                analysisAnswer.ExpectedWrongRequirementsCount - properWrongRequirements.Count;

            var mistakenExplanationCount = 0;

            foreach (var requirement in properWrongRequirements)
            {
                var standartRequirement =
                    task.StandardAnswer.WrongRequirements.First(req => req.Id == requirement.RequirementId);

                if (standartRequirement.ExplanationId != requirement.ExplanationId)
                {
                    mistakenExplanationCount++;
                }
            }

            return mistakenCorrectRequirementsCount + (mistakenExplanationCount/2.0);
        }

        private static int CalculateLostPointsByCorrectRequirements(AnalysisAnswer analysisAnswer,
            TaskWithAnalysisStandartAnswer task)
        {
            var properCorrectRequirementsCount = analysisAnswer.CorrectRequirements.Count(answerId =>
                task.StandardAnswer.CorrectRequirementIds.Any(standartId => standartId == answerId));

            var mistakenCorrectRequirementsCount =
                task.StandardAnswer.CorrectRequirementIds.Count() - properCorrectRequirementsCount;

            return mistakenCorrectRequirementsCount;
        }
    }
}
