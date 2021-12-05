using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Implementations
{
    public class FileExtensionService : IService<FileExtension>
    {
        private IRepository<FileExtension> _fileExtensionRepository;

        public FileExtensionService(IRepository<FileExtension> fileExtensionRepository)
        {
            _fileExtensionRepository = fileExtensionRepository;
        }

        public void Add(FileExtension item)
        {
            _fileExtensionRepository.Add(item);
        }

        public void Delete(int id)
        {
            _fileExtensionRepository.Delete(id);
        }

        public IEnumerable<FileExtension> GetAll()
        {
            var fileExtensions = _fileExtensionRepository.GetAll();

            return fileExtensions;
        }

        public FileExtension GetById(int id)
        {
            var fileExtension = _fileExtensionRepository.GetById(id);

            return fileExtension;
        }

        public void Update(int id, FileExtension newItem)
        {
            _fileExtensionRepository.Update(id, newItem);
        }
    }
}
