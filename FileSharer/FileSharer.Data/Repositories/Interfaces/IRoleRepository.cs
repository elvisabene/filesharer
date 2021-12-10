using FileSharer.Common.Entities;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }
}
