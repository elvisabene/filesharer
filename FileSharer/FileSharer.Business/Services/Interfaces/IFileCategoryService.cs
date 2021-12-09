using FileSharer.Common.Entities;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IFileCategoryService : IService<FileCategory>
    {
        FileCategory GetByName(string name);
    }
}
