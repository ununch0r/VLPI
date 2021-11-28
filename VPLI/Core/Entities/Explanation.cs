using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class Explanation
    {
        public Explanation()
        {
            Requirement = new HashSet<Requirement>();
        }

        public int Id { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }
    }
}
