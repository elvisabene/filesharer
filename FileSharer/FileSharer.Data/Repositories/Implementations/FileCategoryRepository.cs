using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Data.DataConverters;
using FileSharer.Data.Infrastructure;
using FileSharer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FileSharer.Data.Repositories.Implementations
{
    public class FileCategoryRepository : IRepository<FileCategory>
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
            var procedure = DatabaseConstants.StoredProcedureName.AddFileCategory;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            var procedure = DatabaseConstants.StoredProcedureName.DeleteFileCategory;

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

            var fileCategory = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return fileCategory;
        }

        public void Update(int id, FileCategory newItem)
        {
            var procedure = DatabaseConstants.StoredProcedureName.UpdateFileCategory;
            var parameters = _converter.ConvertToSqlParameters(newItem);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
