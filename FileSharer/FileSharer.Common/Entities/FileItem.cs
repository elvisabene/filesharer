using System;

public class FileItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int FileExtensionId { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }

    public int FileCategoryId { get; set; }

    public DateTime CreateDate { get; set; }
}