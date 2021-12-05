using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerResult
{
    public class WritingTaskResult
    {
        public string SystemName { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public ICollection<WrittenRequirementTemplateAnswer> Requirements { get; set; }
    }
}
