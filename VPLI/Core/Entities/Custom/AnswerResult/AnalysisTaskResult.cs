using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerResult
{
    public class AnalysisTaskResult
    {
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public ICollection<string> CorrectRequirements { get; set; }
        public ICollection<WrongRequirementDisplay> WrongRequirements { get; set; }
    }
}
