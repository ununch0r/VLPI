using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Vlpi.Web.Models
{
    public partial class ExecutionMode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short ExecutionTime { get; set; }
        public byte? WrongRequirementsCount { get; set; }
    }
}
