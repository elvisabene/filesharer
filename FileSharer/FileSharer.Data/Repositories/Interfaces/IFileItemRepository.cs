using System.Collections.Generic;

public interface IFileItemRepository : IRepository<FileItem>
{
    IEnumerable<FileItem> GetAllByUserId(int userId);

    IEnumerable<FileItem> GetAllByCategoryId(int categoryId);
}