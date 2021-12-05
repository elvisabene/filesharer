using System.ComponentModel.DataAnnotations;
using FileSharer.Common.Constants;

namespace FileSharer.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Password { get; set; }
    }
}
