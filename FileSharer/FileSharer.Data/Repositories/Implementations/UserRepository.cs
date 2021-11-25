using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace FileSharer.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllByName(string name)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, User newItem)
        {
            throw new NotImplementedException();
        }
    }
}
