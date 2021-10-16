using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerTemplates
{
    public class ModifiedWrongRequirementTemplate
    {
        public int Id { get; set; }
        public IEnumerable<string> AcceptableModifications { get; set; }
    }
}
