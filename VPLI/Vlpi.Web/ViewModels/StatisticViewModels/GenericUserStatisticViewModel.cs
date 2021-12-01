namespace Vlpi.Web.ViewModels.StatisticViewModels
{
    public class GenericUserStatisticViewModel
    {
        public int Attempts { get; set; }
        public int PassedAttempts { get; set; }
        public double AverageScore { get; set; }
        public int AverageTime { get; set; }
    }
}
