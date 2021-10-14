using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            StandardAnswer = new HashSet<StandardAnswer>();
            TaskTip = new HashSet<TaskTip>();
            UserAnswer = new HashSet<UserAnswer>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        [ForeignKey(nameof(TypeId))]
        [InverseProperty(nameof(TaskType.Task))]
        public virtual TaskType Type { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<Requirement> Requirement { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<StandardAnswer> StandardAnswer { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<TaskTip> TaskTip { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
    }
}
