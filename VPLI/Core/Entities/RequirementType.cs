using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public partial class RequirementType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
