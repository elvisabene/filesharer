using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Implementations
{
    public class FileItemRepository : IFileItemRepository
    {
        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
