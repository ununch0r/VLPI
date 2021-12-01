using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public interface IAnswerManager
    {
        Task<WritingTaskResult> VerifyWritingAnswerAsync(int userId, WritingAnswer writingAnswer);
        Task<AnalysisTaskResult> VerifyAndSaveAnalysisAnswerAsync(int userId, AnalysisAnswer analysisAnswer);
        Task<ICollection<UserAnswer>> GetAnswersByUserAsync(int userId);
        Task<ICollection<UserAnswer>> GetAnswersByTaskAsync(int taskId);
        Task<ICollection<UserAnswer>> GetAllAnswersAsync();
    }
}
