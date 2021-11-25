using System.Collections.Generic;

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

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
