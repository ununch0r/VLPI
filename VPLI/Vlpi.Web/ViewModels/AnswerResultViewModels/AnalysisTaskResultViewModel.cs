using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerResultViewModels
{
    public class AnalysisTaskResultViewModel
    {
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public ICollection<string> CorrectRequirements { get; set; }
        public ICollection<WrongRequirementDisplayViewModel> WrongRequirements { get; set; }
    }
}
