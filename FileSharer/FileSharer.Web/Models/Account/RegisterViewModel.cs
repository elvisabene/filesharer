using FileSharer.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [Compare("Password", ErrorMessage = ErrorMessages.PasswordsDontMatch)]
        public string ConfirmPassword { get; set; }
    }
}
