using System.Collections.Generic;

namespace Core.Entities.Custom.AnswerModels
{
    public class WritingAnswer : Answer
    {
        public string SystemName { get; set; }
        public ICollection<WritingRequirement> Requirements { get; set; }
    }
}
