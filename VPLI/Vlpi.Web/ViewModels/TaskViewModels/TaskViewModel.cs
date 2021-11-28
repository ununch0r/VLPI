using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public TaskTypeViewModel Type { get; set; }
        public ICollection<ExplanationViewModel> Explanation { get; set; }
        public ICollection<RequirementViewModel> Requirement { get; set; }
        public ICollection<TaskTipViewModel> TaskTip { get; set; }
    }
}
