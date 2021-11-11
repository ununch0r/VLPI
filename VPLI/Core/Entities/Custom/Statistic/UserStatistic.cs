using System.Collections.Generic;

namespace Core.Entities.Custom.Statistic
{
    public class UserStatistic
    {
        public int UserId { get; set; }
        public ICollection<UserTaskStatistic> UserTaskStatistics { get; set; }
    }
}
