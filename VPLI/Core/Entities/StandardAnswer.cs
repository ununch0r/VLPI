using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class StandardAnswer
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string AnswerTemplate { get; set; }

        [InverseProperty("IdNavigation")]
        public virtual Task Task { get; set; }
    }
}
