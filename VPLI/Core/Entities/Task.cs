using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Task
    {
        public Task()
        {
            Requirement = new HashSet<Requirement>();
            TaskTip = new HashSet<TaskTip>();
            UserAnswer = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string StandardAnswer { get; set; }

        public virtual TaskType Type { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<TaskTip> TaskTip { get; set; }
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
    }
}
