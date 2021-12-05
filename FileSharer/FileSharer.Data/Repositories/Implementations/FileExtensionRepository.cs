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
    public class FileExtensionRepository : IRepository<FileExtension>
    {
        public IDataContext _dataContext;

        public IDataConverter<FileExtension> _converter;

        public FileExtensionRepository(IDataContext dataContext, IDataConverter<FileExtension> converter)
        {
            _dataContext = dataContext;
            _converter = converter;
        }

        public void Add(FileExtension item)
        {
            var parameters = _converter.ConvertToSqlParameters(item);
            var procedure = DatabaseConstants.StoredProcedureName.ForAdd.FileExtension;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            _dataContext.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            var procedure = DatabaseConstants.StoredProcedureName.ForDelete.FileExtension;

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }

        public IEnumerable<FileExtension> GetAll()
        {
            var view = DatabaseConstants.ViewName.AllFileExtensions;
            var query = $"SELECT * FROM {view}";

            SqlCommand command = new SqlCommand(query);

            var fileExtensions = _dataContext.ExecuteQueryAsList(command, _converter);

            return fileExtensions;
        }

        public FileExtension GetById(int id)
        {
            var view = DatabaseConstants.ViewName.AllFileExtensions;
            var query = $"SELECT * FROM {view} " +
                        $"WHERE Id = @id";

            SqlCommand command = new SqlCommand(query);

            var fileExtension = _dataContext.ExecuteQueryAsSingle(command, _converter);

            return fileExtension;
        }

        public void Update(int id, FileExtension newItem)
        {
            var procedure = DatabaseConstants.StoredProcedureName.ForUpdate.FileExtension;
            var parameters = _converter.ConvertToSqlParameters(newItem);

            SqlCommand command = new SqlCommand(procedure);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);
            command.Parameters.AddWithValue("@id", id);

            _dataContext.ExecuteNonQuery(command);
        }
    }
}
