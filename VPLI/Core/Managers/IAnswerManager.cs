using System.Threading.Tasks;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;

namespace Core.Managers
{
    public interface IAnswerManager
    {
        Task<WritingTaskResult> VerifyWritingAnswerAsync(int userId, WritingAnswer writingAnswer);
        Task<AnalysisTaskResult> VerifyAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer);
    }
}
