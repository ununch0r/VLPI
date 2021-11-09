using System.Threading.Tasks;
using Core.Entities.Custom.AnswerModels;
using Core.Entities.Custom.AnswerResult;
using Core.Managers;

namespace Business.Managers
{
    public class AnswerManager : IAnswerManager
    {
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
