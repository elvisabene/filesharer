using FileSharer.Common.Entities;
using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IFileItemRepository : IRepository<FileItem>
    {
        IEnumerable<FileItem> GetAllByUserId(int userId);

        IEnumerable<FileItem> GetAllByCategoryId(int categoryId);

        IEnumerable<FileItem> GetAllByName(string name);

        void IncrementDownloadsCount(int id);
    }
}
