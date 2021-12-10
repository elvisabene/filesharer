using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;
namespace FileSharer.Business.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Add(Role role)
        {
            _roleRepository.Add(role);
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }

        public IEnumerable<Role> GetAll()
        {
            var roles = _roleRepository.GetAll();

            return roles;
        }

        public Role GetById(int id)
        {
            var role = _roleRepository.GetById(id);

            return role;
        }

        public Role GetByName(string name)
        {
            var role = _roleRepository.GetByName(name);

            return role;
        }

        public void Update(int id, Role newRole)
        {
            _roleRepository.Update(id, newRole);
        }
    }
}
