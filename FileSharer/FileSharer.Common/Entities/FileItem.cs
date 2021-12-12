using System;

namespace FileSharer.Common.Entities
{
    public class FileItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FileExtensionId { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int Size { get; set; }

        public int DownloadsCount { get; set; }

        public int UserId { get; set; }

        public int FileCategoryId { get; set; }
    }
}
