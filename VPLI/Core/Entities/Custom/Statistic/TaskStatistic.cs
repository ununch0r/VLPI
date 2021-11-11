namespace Core.Entities.Custom.Statistic
{
    public class TaskStatistic
    {
        public int TaskId { get; set; }
        public int Order { get; set; }
        public string Objective { get; set; }
        public int Complexity { get; set; }
        public int UserAnswersCount { get; set; }
        public double AverageScore { get; set; }
        public int AverageTime { get; set; }
    }
}
