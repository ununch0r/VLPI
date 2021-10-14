using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
