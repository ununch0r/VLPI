using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class RequirementsAnalysisTaskTemplateAnswer
    {
        public IEnumerable<int> CorrectRequirementIds { get; set; }
        public IEnumerable<WrongRequirementTemplate> WrongRequirements { get; set; }
    }
}
