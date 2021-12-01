using System;

namespace Vlpi.Web.ViewModels.StatisticViewModels
{
    public class UserTaskStatisticViewModel
    {
        public int AnswerId { get; set; }
        public string TaskType { get; set; }
        public string Objective { get; set; }
        public int TimeSpent { get; set; }
        public DateTime DatePassed { get; set; }
        public double Score { get; set; }
    }
}
