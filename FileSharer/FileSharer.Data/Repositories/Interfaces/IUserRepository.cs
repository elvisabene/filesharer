using FileSharer.Common.Entities;
using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);

        IEnumerable<User> GetAllByName(string name);
    }
}
