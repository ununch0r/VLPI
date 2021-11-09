using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerModels
{
    public class AnalysisAnswer : Answer
    {
        public ICollection<int> CorrectRequirements { get; set; }

        public ICollection<ModifiedRequirement> WrongRequirements { get; set; }
    }
}
