using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class TaskTip
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public byte Order { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("TaskTip")]
        public virtual Task Task { get; set; }
    }
}
