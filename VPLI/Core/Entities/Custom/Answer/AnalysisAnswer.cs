using System.Collections.Generic;

namespace Core.Entities.Custom.Answer
{
    public class AnalysisAnswer : Answer
    {
        public ICollection<int> CorrectRequirements { get; set; }
        public ICollection<int> WrongRequirements { get; set; }

        //public ICollection<ModifiedRequirement> WrongRequirements { get; set; }
    }
}
