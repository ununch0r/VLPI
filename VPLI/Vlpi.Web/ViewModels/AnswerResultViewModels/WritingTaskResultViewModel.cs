using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerResultViewModels
{
    public class WritingTaskResultViewModel
    {
        public string SystemName { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public ICollection<WrittenRequirementTemplateAnswerViewModel> Requirements { get; set; }
    }
}
