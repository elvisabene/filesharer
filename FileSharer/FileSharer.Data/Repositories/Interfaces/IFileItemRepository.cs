using FileSharer.Common.Entities;
using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IFileItemRepository
    {
        IEnumerable<FileItem> GetAllByUserId(int userId);

        IEnumerable<FileItem> GetAllByCategoryId(int categoryId);
    }
}
