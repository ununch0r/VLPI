using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public short TypeId { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<RequirementViewModel> Requirement { get; set; }
        public ICollection<TaskTipViewModel> TaskTip { get; set; }
    }
}
