namespace Vlpi.Web.ViewModels.StatisticViewModels
{
    public class TaskStatisticViewModel
    {
        public int Order { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public int UserAnswersCount { get; set; }
        public double AverageScore { get; set; }
        public int AverageTime { get; set; }
    }
}
