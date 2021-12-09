using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Implementations
{
    public class FileCategoryService : IFileCategoryService
    {
        private IFileCategoryService _fileCategoryRepository;

        public FileCategoryService(IFileCategoryService fileCategoryRepository)
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
            var fileCategory = _fileCategoryRepository.GetById(id);

            return fileCategory;
        }

        public FileCategory GetByName(string name)
        {
            var fileCategory = _fileCategoryRepository.GetByName(name);

            return fileCategory;
        }

        public void Update(int id, FileCategory newItem)
        {
            _fileCategoryRepository.Update(id, newItem);
        }
    }
}
