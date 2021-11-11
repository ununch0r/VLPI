using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Core.Entities
{
    public partial class UserAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public short TimeSpent { get; set; }
        public byte Score { get; set; }
        public string Answer { get; set; }
        public DateTime ExecutionDate { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
