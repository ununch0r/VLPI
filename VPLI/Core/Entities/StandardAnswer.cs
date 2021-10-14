using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class StandardAnswer
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        [Required]
        public string AnswerTemplate { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("StandardAnswer")]
        public virtual Task Task { get; set; }
    }
}
