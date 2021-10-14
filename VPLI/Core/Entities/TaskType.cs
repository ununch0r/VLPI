using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
