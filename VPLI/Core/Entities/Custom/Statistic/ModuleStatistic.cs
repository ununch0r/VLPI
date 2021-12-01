using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Custom.Statistic
{
    public class ModuleStatistic
    {
        public int Complexity { get; set; }
        public int UserAnswersCount { get; set; }
        public double AverageScore { get; set; }
        public int AverageTime { get; set; }
    }
}
