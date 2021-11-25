using System.Collections.Generic;

public interface IUserRepository : IRepository<User>
{
    User GetByEmail(string email);

    IEnumerable<User> GetAllByName(string name);
}