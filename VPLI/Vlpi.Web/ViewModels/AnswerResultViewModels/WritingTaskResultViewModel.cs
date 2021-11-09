using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerResultViewModels
{
    public class WritingTaskResultViewModel
    {
        public int Score { get; set; }
        public ICollection<WrittenRequirementTemplateAnswerViewModel> Requirements { get; set; }
    }
}
