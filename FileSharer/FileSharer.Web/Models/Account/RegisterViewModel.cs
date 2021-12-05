using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Compare("Password", ErrorMessage = "Passwords dont match!")]
        public string ConfirmPassword { get; set; }
    }
}
