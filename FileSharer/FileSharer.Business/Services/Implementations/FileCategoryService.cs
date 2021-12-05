using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Implementations
{
    public class FileCategoryService : IService<FileCategory>
    {
        private IRepository<FileCategory> _fileCategoryRepository;

        public FileCategoryService(IRepository<FileCategory> fileCategoryRepository)
        {
            _fileCategoryRepository = fileCategoryRepository;
        }

        public void Add(FileCategory item)
        {
            _fileCategoryRepository.Add(item);
        }

        public void Delete(int id)
        {
            _fileCategoryRepository.Delete(id);
        }

        public IEnumerable<FileCategory> GetAll()
        {
            var fileExtensions = _fileCategoryRepository.GetAll();

            return fileExtensions;
        }

        public FileCategory GetById(int id)
        {
            var fileExtension = _fileCategoryRepository.GetById(id);

            return fileExtension;
        }

        public void Update(int id, FileCategory newItem)
        {
            _fileCategoryRepository.Update(id, newItem);
        }
    }
}
