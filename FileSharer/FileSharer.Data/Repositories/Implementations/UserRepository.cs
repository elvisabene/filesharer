using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Data.DataConverters;
using FileSharer.Data.Infrastructure;
using FileSharer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private IDataContext _dataContext;

        public UserRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(User user)
        {
            string procedure = DatabaseConstants.StoredProcedureName.AddUser;
            SqlParameter[] parameters = user.ConvertToSqlParameters();

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

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var users = reader.ConvertToUserList();

            return users;
        }

        public IEnumerable<User> GetAllByName(string name)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Name = @name";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@name", name);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var users = reader.ConvertToUserList();

            return users;
        }

        public User GetByEmail(string email)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Email = @email";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@email", email);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var user = reader.ConvertToUser();

            return user;
        }

        public User GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllUsers;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var user = reader.ConvertToUser();

            return user;
        }

        public void Update(int id, User newUser)
        {
            string procedure = DatabaseConstants.StoredProcedureName.UpdateUser;
            SqlParameter[] parameters = newUser.ConvertToSqlParameters();

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
