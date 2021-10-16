using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class WritingRequirementsTaskTemplateAnswer
    {
        public IEnumerable<string> AcceptableSystemNames { get; set; }
        public IEnumerable<WrittenRequirementTemplateAnswer> TemplateAnswers { get; set; }
    }
}
