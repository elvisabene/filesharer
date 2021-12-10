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
            var users = _fileItemRepository.GetAll();

            return users;
        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            var users = _fileItemRepository.GetAllByCategoryId(categoryId);

            return users;
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            var users = _fileItemRepository.GetAllByUserId(userId);

            return users;
        }

        public FileItem GetById(int id)
        {
            var user = _fileItemRepository.GetById(id);

            return user;
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
