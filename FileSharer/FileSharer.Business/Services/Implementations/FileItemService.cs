using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Implementations
{
    public class FileItemService : IFileItemService
    {
        private readonly IFileItemRepository _fileItemRepository;

        public FileItemService(IFileItemRepository fileItemRepository)
        {
            _fileItemRepository = fileItemRepository;
        }

        public void Add(FileItem fileItem)
        {
            _fileItemRepository.Add(fileItem);
        }

        public void Delete(int id)
        {
            _fileItemRepository.Delete(id);
        }

        public IEnumerable<FileItem> GetAll()
        {
            var files = _fileItemRepository.GetAll();

            return files;
        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            var files = _fileItemRepository.GetAllByCategoryId(categoryId);

            return files;
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            var files = _fileItemRepository.GetAllByUserId(userId);

            return files;
        }

        public IEnumerable<FileItem> GetAllByName(string name)
        {
           var files = _fileItemRepository.GetAllByName(name);

            return files;
        }

        public FileItem GetById(int id)
        {
            var file = _fileItemRepository.GetById(id);

            return file;
        }

        public void Update(int id, FileItem newFileItem)
        {
            _fileItemRepository.Update(id, newFileItem);
        }

        public void IncrementDownloadsCount(int id)
        {
            _fileItemRepository.IncrementDownloadsCount(id);
        }
    }
}
