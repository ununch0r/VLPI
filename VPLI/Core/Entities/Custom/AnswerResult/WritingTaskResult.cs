using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerResult
{
    public class WritingTaskResult
    {
        public int Score { get; set; }
        public ICollection<WrittenRequirementTemplateAnswer> Requirements { get; set; }
    }
}
