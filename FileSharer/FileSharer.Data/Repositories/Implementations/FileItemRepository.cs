using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Data.Database;
using FileSharer.Data.DataConverters;
using FileSharer.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.Repositories.Implementations
{
    public class FileItemRepository : IFileItemRepository
    {
        private readonly IDatabaseSettings _dbSettings;

        public FileItemRepository(IDatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        public void Add(FileItem fileItem)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                string query = DatabaseConstants.StoredProcedureName.AddFileItem;
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = fileItem.ConvertToSqlParameters();

                command.Parameters.AddRange(parameters);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = DatabaseConstants.StoredProcedureName.DeleteFileItem;

            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<FileItem> GetAll()
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}";


            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                IEnumerable<FileItem> fileItems = dataReader.ConvertToFileItemList();
                return fileItems;
            }
        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE CategoryId = @categoryId";
            ;

            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                IEnumerable<FileItem> fileItems = dataReader.ConvertToFileItemList();

                return fileItems;
            }
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE CategoryId = @categoryId";

            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                IEnumerable<FileItem> fileItems = dataReader.ConvertToFileItemList();

                return fileItems;
            }
        }

        public FileItem GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE CategoryId = @categoryId";

            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                FileItem fileItem = dataReader.ConvertToFileItem();

                return fileItem;
            }
        }

        public void Update(int id, FileItem newFileItem)
        {
            string query = DatabaseConstants.StoredProcedureName.DeleteFileItem;

            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter[] parameters = newFileItem.ConvertToSqlParameters();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
        }
    }
}
