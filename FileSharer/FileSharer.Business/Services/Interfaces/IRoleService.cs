using FileSharer.Common.Entities;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IRoleService : IService<Role>
    {
        Role GetByName(string name);
    }
}
