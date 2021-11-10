using System.Threading.Tasks;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
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

        public Task<AnalysisTaskResult> VerifyAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer)
        {
            throw new System.NotImplementedException();
        }
    }
}
