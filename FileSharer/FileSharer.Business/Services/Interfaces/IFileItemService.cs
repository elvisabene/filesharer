using FileSharer.Common.Entities;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IFileItemService : IService<FileItem>
    {
        IEnumerable<FileItem> GetAllByUserId(int userId);

        IEnumerable<FileItem> GetAllByCategoryId(int categoryId);

        void IncrementDownloadsCount(int id);
    }
}
