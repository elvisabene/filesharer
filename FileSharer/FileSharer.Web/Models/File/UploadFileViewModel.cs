using FileSharer.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.File
{
    public class UploadFileViewModel
    {
        [Required (ErrorMessage = ErrorMessage.RequiredField)]
        public string Category { get; set; }

        public string Description { get; set; }

        [Required (ErrorMessage = ErrorMessage.RequiredField)]
        public IFormFile File { get; set; }
    }
}
