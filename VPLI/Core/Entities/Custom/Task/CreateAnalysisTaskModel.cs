using System.Collections.Generic;

namespace Core.Entities.Custom.Task
{
    public class CreateWritingTaskModel
    {
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }

        public string PhotoUrl { get; set; }
        public IList<string> SystemNames { get; set; }
        public IList<Requirement> Requirements { get; set; }
        public IList<TaskTip> TaskTip { get; set; }
    }
}
