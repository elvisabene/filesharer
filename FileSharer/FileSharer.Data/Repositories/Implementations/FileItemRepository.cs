using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Data.Database;
using FileSharer.Data.Repositories.Interfaces;
using System;
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

        public void Add(FileItem file)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                string sqlQuery = DatabaseConstants.StoredProcedureName.AddFileItem;
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters =
                {
                    new SqlParameter()
                    {
                        ParameterName = "@name",
                        Value = file.Name,
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@fileExtensionId",
                        Value = file.FileExtensionId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@description",
                        Value = file.Description,
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@userId",
                        Value = file.UserId,
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@fileCategoryId",
                        Value = file.FileCategoryId,
                    }
                };

                sqlCommand.Parameters.AddRange(parameters);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileItem> GetAllByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileItem> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public FileItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, FileItem newItem)
        {
            throw new NotImplementedException();
        }
    }
}
