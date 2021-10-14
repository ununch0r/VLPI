using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Requirement
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("Requirement")]
        public virtual Task Task { get; set; }
    }
}
