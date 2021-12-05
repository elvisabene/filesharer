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

        private readonly IDataConverter<FileItem> _converter;

        public FileItemRepository(IDataContext dataContext, IDataConverter<FileItem> converter)
        {
            _dataContext = dataContext;
            _converter = converter;
        }

        public void Add(FileItem fileItem)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForAdd.FileItem;
            SqlParameter[] parameters = _converter.ConvertToSqlParameters(fileItem);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForDelete.FileItem;

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

            var fileItems = _dataContext.ExecuteQueryAsList(command, _converter);

            return fileItems;
        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view} " +
                           $"WHERE CategoryId = @categoryId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@categoryId", categoryId);
            
            var fileItems = _dataContext.ExecuteQueryAsList(command, _converter);

            return fileItems;
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view} " +
                           $"WHERE UserId = @userId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@userId", userId);

            var fileItems = _dataContext.ExecuteQueryAsList(command, _converter);

            return fileItems;
        }

        public FileItem GetById(int id)
        {
            string view = DatabaseConstants.ViewName.AllFileItems;
            string query = $"SELECT * FROM {view} " +
                           $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            var fileItem = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return fileItem;
        }

        public void Update(int id, FileItem newFileItem)
        {
            string procedure = DatabaseConstants.StoredProcedureName.ForUpdate.FileItem;
            SqlParameter[] parameters = _converter.ConvertToSqlParameters(newFileItem);

            SqlCommand command = new SqlCommand(procedure);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
