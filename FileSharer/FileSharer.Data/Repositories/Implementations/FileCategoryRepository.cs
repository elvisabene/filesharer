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
    public class FileCategoryRepository : IFileCategoryRepository
    {
        public IDataContext _dataContext;

        public IDataConverter<FileCategory> _converter;

        public FileCategoryRepository(IDataContext dataContext, IDataConverter<FileCategory> converter)
        {
            _dataContext = dataContext;
            _converter = converter;
        }

        public void Add(FileCategory item)
        {
            var parameters = _converter.ConvertToSqlParameters(item);
            var procedure = DatabaseConstants.StoredProcedureName.ForAdd.FileCategory;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            var procedure = DatabaseConstants.StoredProcedureName.ForDelete.FileCategory;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }

        public IEnumerable<FileCategory> GetAll()
        {
            var view = DatabaseConstants.ViewName.AllFileCategories;
            var query = $"SELECT * FROM {view}";

            SqlCommand command = new SqlCommand(query);

            var fileCategories = _dataContext.ExecuteQueryAsList(command, _converter);

            return fileCategories;
        }

        public FileCategory GetById(int id)
        {
            var view = DatabaseConstants.ViewName.AllFileCategories;
            var query = $"SELECT * FROM {view} " +
                        $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            var fileCategory = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return fileCategory;
        }

        public FileCategory GetByName(string name)
        {
            var view = DatabaseConstants.ViewName.AllFileCategories;
            var query = $"SELECT * FROM {view} " +
                        $"WHERE Name = @name";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@name", name);

            var fileCategory = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return fileCategory;
        }

        public void Update(int id, FileCategory newItem)
        {
            var procedure = DatabaseConstants.StoredProcedureName.ForUpdate.FileCategory;
            var parameters = _converter.ConvertToSqlParameters(newItem);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
