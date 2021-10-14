using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class TaskType
    {
        public TaskType()
        {
            Task = new HashSet<Task>();
        }

        [Key]
        public short Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Task> Task { get; set; }
    }
}
