using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }
    }
}
