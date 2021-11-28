using System.Collections.Generic;

namespace Core.Entities.Custom.Answer
{
    public class AnalysisAnswer : Answer
    {
        public ICollection<int> CorrectRequirements { get; set; }
        public ICollection<WrongRequirement> WrongRequirements { get; set; }
    }
}
