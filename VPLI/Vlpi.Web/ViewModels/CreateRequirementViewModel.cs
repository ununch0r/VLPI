using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels
{
    public class CreateRequirementViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
