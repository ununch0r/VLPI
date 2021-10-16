using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class RequirementsAnalysisTaskTemplateAnswer
    {
        public IEnumerable<int> CorrectRequirementIds { get; set; }
        public IEnumerable<ModifiedWrongRequirementTemplate> ModifiedRequirements { get; set; }
    }
}
