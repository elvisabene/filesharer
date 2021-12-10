using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace FileSharer.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            var users = _userRepository.GetAll();

            return users;
        }

        public IEnumerable<User> GetAllByName(string name)
        {
            var users = _userRepository.GetAllByName(name);

            return users;
        }

        public User GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            return user;
        }

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id);

            return user;
        }

        public void Update(int id, User newUser)
        {
            newUser.PasswordHash = _userRepository.GetById(id).PasswordHash;
            _userRepository.Update(id, newUser);
        }
    }
}
