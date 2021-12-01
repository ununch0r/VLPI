namespace Core.Entities.Custom.Statistic
{
    public class GenericUserStatistic
    {
        public int Attempts { get; set; }
        public int PassedAttempts { get; set; }
        public double AverageScore { get; set; }
        public int AverageTime { get; set; }
    }
}
