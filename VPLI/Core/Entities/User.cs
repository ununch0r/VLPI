using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class User
    {
        public User()
        {
            UserAnswer = new HashSet<UserAnswer>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        //TODO: typo
        [Required]
        [StringLength(128)]
        public string HashedPasswrod { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
