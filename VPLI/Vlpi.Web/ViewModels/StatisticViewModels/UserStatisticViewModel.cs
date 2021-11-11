using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.StatisticViewModels
{
    public class UserStatisticViewModel
    {
        public int UserId { get; set; }
        public ICollection<UserTaskStatisticViewModel> UserTaskStatistics { get; set; }
    }
}
