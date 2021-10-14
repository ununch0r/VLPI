using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [StringLength(128)]
        public string HashedPasswrod { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
