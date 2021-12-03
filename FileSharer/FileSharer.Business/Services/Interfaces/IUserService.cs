using FileSharer.Common.Entities;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        User GetByEmail(string email);

        IEnumerable<User> GetAllByName(string name);
    }
}
