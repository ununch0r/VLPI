using System.ComponentModel.DataAnnotations;

namespace Vlpi.Web.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
