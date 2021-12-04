using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class CreateWritingTaskViewModel
    {
        [Required]
        [StringLength(255)]
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }

        public string PhotoUrl { get; set; }
        public IList<string> SystemNames { get; set; }
        public IList<CreateRequirementViewModel> Requirements { get; set; }
        public IList<CreateTaskTipViewModel> TaskTip { get; set; }
    }
}
