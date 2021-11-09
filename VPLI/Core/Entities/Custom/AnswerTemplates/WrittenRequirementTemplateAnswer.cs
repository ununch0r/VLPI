using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class WrittenRequirementTemplateAnswer
    {
        public int RequirementTypeId { get; set; }
        public IEnumerable<string> PossibleRequirementStatements { get; set; }
    }
}
