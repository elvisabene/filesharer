using System;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models.File
{
    public class FileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Size { get; set; }

        public int DownloadsCount { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}
