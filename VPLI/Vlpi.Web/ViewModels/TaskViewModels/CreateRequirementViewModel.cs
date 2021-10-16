using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class CreateRequirementViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
