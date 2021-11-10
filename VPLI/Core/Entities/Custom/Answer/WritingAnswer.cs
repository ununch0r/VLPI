using System.Collections.Generic;

namespace Core.Entities.Custom.Answer
{
    public class WritingAnswer : Answer
    {
        public string SystemName { get; set; }
        public ICollection<WritingRequirement> Requirements { get; set; }
    }
}
