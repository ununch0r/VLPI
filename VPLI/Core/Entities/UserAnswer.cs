using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
