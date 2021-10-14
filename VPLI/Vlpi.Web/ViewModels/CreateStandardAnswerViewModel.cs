using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels
{
    public class CreateStandardAnswerViewModel
    {
        [Required]
        public string AnswerTemplate { get; set; }
    }
}
