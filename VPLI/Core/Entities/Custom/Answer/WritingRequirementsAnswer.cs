using System.Collections.Generic;

namespace Core.Entities.Custom.Answer
{
    public class WritingRequirementsAnswer
    {
        public string SystemName { get; set; }
        public IEnumerable<WrittenRequirementAnswer> TemplateAnswers { get; set; }
    }
}
