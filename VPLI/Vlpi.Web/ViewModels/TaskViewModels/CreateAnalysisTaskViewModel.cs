using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class CreateAnalysisTaskViewModel
    {
        [Required]
        [StringLength(255)]
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }

        public IList<CreateRequirementViewModel> CorrectRequirements { get; set; }
        public IList<CreateRequirementViewModel> WrongRequirements { get; set; }
        public IList<CreateTaskTipViewModel> TaskTip { get; set; }
    }
}
