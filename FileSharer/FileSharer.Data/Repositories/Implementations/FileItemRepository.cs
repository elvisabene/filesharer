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
    public class FileItemRepository : IFileItemRepository
    {
        private readonly IDataContext _dataContext;

        public FileItemRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(FileItem fileItem)
        {
            string procedure = DatabaseConstants.StoredProcedureName.AddFileItem;
            SqlParameter[] parameters = fileItem.ConvertToSqlParameters();

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            string procedure = DatabaseConstants.StoredProcedureName.DeleteFileItem;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }

        public IEnumerable<FileItem> GetAll()
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}";

            SqlCommand command = new SqlCommand(query);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var fileItems = reader.ConvertToFileItemList();

            return fileItems;

        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE CategoryId = @categoryId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@categoryId", categoryId);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var fileItems = reader.ConvertToFileItemList();

            return fileItems;
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE UserId = @userId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@userId", userId);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var fileItems = reader.ConvertToFileItemList();

            return fileItems;
        }

        public FileItem GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view}" +
                           $"WHERE CategoryId = @categoryId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = _dataContext.ExecuteQuery(command);
            var fileItem = reader.ConvertToFileItem();

            return fileItem;
        }

        public void Update(int id, FileItem newFileItem)
        {
            string procedure = DatabaseConstants.StoredProcedureName.UpdateFileItem;
            SqlParameter[] parameters = newFileItem.ConvertToSqlParameters();

            SqlCommand command = new SqlCommand(procedure);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
