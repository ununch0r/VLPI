using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerResult
{
    public class AnalysisTaskResult
    {
        public int Score { get; set; }
        public ICollection<string> CorrectRequirements { get; set; }
        public ICollection<string> ModifiedRequirements { get; set; }
    }
}
