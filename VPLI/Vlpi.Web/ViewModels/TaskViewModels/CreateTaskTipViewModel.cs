using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.TaskViewModels
{
    public class CreateTaskTipViewModel
    {
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public byte Order { get; set; }
    }
}
