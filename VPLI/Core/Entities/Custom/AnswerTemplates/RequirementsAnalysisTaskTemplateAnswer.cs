using System.Collections.Generic;
using Core.Entities.Custom.Answer;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class RequirementsAnalysisTaskTemplateAnswer
    {
        public IEnumerable<int> CorrectRequirementIds { get; set; }
        public IEnumerable<WrongRequirement> WrongRequirements { get; set; }
    }
}
