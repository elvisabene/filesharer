using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Data.DataConverters;
using FileSharer.Data.Infrastructure;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDataConverter<Role> _converter;

        private readonly IDataContext _dataContext;

        public RoleRepository(IDataConverter<Role> converter, IDataContext dataContext)
        {
            _converter = converter;
            _dataContext = dataContext;
        }

        public void Add(Role role)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForAdd.Role;
            var parameters = _converter.ConvertToSqlParameters(role);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForDelete.Role;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }

        public IEnumerable<Role> GetAll()
        {
            string view = DatabaseConstants.ViewName.AllRoles;
            string query = $"SELECT * FROM {view}";

            SqlCommand command = new SqlCommand(query);
            var roles = _dataContext.ExecuteQueryAsList(command, _converter);

            return roles;
        }

        public Role GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllRoles;
            string query = $"SELECT * FROM {view} " +
                           $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            var role = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return role;
        }

        public Role GetByName(string name)
        {
            string view = DatabaseConstants.ViewName.AllRoles;
            string query = $"SELECT * FROM {view} " +
                           $"WHERE Name = @name";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@name", name);

            var role = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return role;
        }

        public void Update(int id, Role newRole)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForUpdate.Role;
            var parameters = _converter.ConvertToSqlParameters(newRole);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
