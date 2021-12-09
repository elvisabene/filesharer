using FileSharer.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.File
{
    public class EditFileViewModel
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage = ErrorMessage.RequiredField)]
        public string NewName { get; set; }

        [Required  (ErrorMessage = ErrorMessage.RequiredField)]
        public string NewCategory { get; set; }
        
        public string NewDescription { get; set; }
    }
}
