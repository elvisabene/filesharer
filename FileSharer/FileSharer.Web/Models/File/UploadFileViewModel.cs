using FileSharer.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.File
{
    public class UploadFileViewModel
    {
        [Required (ErrorMessage = ErrorMessages.RequiredField)]
        public string Category { get; set; }

        public string Description { get; set; }

        [Required (ErrorMessage = ErrorMessages.NoFileSelected)]
        public IFormFile File { get; set; }
    }
}
