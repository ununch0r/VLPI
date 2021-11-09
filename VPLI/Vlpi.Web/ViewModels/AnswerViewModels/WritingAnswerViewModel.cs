using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerViewModels
{
    public class WritingAnswerViewModel : AnswerViewModel
    {
        public string SystemName { get; set; }
        public ICollection<WritingRequirementViewModel> Requirements { get; set; }
    }
}
