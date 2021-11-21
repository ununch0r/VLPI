using System.Collections.Generic;

namespace Core.Entities.Custom.Task
{
    public class CreateAnalysisTaskModel
    {
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }

        public IList<Requirement> CorrectRequirements { get; set; }
        public IList<Requirement> WrongRequirements { get; set; }
        public IList<TaskTip> TaskTip { get; set; }
    }
}
