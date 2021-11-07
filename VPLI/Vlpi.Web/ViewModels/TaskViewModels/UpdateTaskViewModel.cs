using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class UpdateTaskViewModel
    {
        [Required]
        [StringLength(255)]
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string StandardAnswer { get; set; }

        public IList<CreateRequirementViewModel> Requirement { get; set; }
        public IList<CreateTaskTipViewModel> TaskTip { get; set; }
    }
}
