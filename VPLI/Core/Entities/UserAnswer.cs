using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class UserAnswer
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public short TimeSpent { get; set; }
        public byte Score { get; set; }
        [Required]
        public string Answer { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("UserAnswer")]
        public virtual Task Task { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserAnswer")]
        public virtual User User { get; set; }
    }
}
