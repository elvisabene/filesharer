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
    public class UserRepository : IUserRepository
    {
        private IDataContext _dataContext;

        private IDataConverter<User> _converter;

        public UserRepository(IDataContext dataContext, IDataConverter<User> converter)
        {
            _dataContext = dataContext;
            _converter = converter;
        }

        public void Add(User user)
        {
            string procedure = DatabaseConstants.StoredProcedureName.AddUser;
            SqlParameter[] parameters = _converter.ConvertToSqlParameters(user);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            string procedure = DatabaseConstants.StoredProcedureName.DeleteUser;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }

        public IEnumerable<User> GetAll()
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}";

            SqlCommand command = new SqlCommand(query);

            var users = _dataContext.ExecuteQueryAsList(command, _converter);

            return users;
        }

        public IEnumerable<User> GetAllByName(string name)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Name = @name";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@name", name);

            var users = _dataContext.ExecuteQueryAsList(command, _converter);

            return users;
        }

        public User GetByEmail(string email)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Email = @email";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@email", email);

            var user = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return user;
        }

        public User GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            var user = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return user;
        }

        public void Update(int id, User newUser)
        {
            string procedure = DatabaseConstants.StoredProcedureName.UpdateUser;
            SqlParameter[] parameters = _converter.ConvertToSqlParameters(newUser);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
