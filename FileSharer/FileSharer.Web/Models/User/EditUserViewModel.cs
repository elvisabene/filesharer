using FileSharer.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.User
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessage.RequiredField)]
        public string Role { get; set; }
    }
}
