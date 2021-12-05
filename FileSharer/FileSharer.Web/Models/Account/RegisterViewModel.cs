using FileSharer.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        [Compare("Password", ErrorMessage = ErrorMessage.PasswordsDontMatch)]
        public string ConfirmPassword { get; set; }
    }
}
