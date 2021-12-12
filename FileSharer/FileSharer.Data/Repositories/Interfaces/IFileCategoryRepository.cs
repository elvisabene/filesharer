using FileSharer.Common.Entities;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IFileCategoryRepository : IRepository<FileCategory>
    {
        public FileCategory GetByName(string name);
    }
}
