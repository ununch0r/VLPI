using System;

namespace Vlpi.Web.ViewModels.StatisticViewModels
{
    public class UserTaskStatisticViewModel
    {
        public int TaskId { get; set; }
        public string TaskType { get; set; }
        public string Objective { get; set; }
        public DateTime DatePassed { get; set; }
        public double Score { get; set; }
    }
}
