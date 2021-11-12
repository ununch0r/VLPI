using System.Collections.Generic;

namespace Core.Entities.Custom.Task
{
    public class TaskCustomModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string StandardAnswer { get; set; }

        public TaskType Type { get; set; }
        public ICollection<Requirement> Requirement { get; set; }
        public ICollection<TaskTip> TaskTip { get; set; }
    }
}
