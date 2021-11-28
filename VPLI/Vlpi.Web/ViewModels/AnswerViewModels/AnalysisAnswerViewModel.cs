using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerViewModels
{
    public class AnalysisAnswerViewModel : AnswerViewModel
    {
        public ICollection<int> CorrectRequirements { get; set; }
        public ICollection<int> WrongRequirements { get; set; }

        //public ICollection<ModifiedRequirementViewModel> WrongRequirements { get; set; }
    }
}
