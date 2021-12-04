using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class CreateRequirementViewModel
    {
        [Required]
        public string Description { get; set; }
        public string Explanation { get; set; }
        public string Continuation { get; set; }
        public int? TypeId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
