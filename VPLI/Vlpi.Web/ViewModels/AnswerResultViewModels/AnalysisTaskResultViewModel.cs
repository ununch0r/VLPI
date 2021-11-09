using System.Collections.Generic;

namespace Vlpi.Web.ViewModels.AnswerResultViewModels
{
    public class AnalysisTaskResultViewModel
    {
        public int Score { get; set; }
        public ICollection<string> CorrectRequirements { get; set; }
        public ICollection<string> ModifiedRequirements { get; set; }
    }
}
