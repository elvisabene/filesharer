using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public class FileItemConverter : IDataConverter<FileItem>
    {
        public FileItem ConvertToSingleItem(SqlDataReader reader, bool withRead = true)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                return null;
            }

            if (withRead)
            {
                reader.Read();
            }

            FileItem fileItem = new FileItem()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                FileExtensionId = (int)reader["ExtensionId"],

                Description = reader["Description"] == DBNull.Value ?
                    string.Empty : (string)reader["Description"],

                UserId = (int)reader["UserId"],
                FileCategoryId = (int)reader["CategoryId"],
            };

            return fileItem;
        }

        public IEnumerable<FileItem> ConvertToItemList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
            }

            var fileItems = new List<FileItem>();

            while (reader.Read())
            {
                var fileItem = ConvertToSingleItem(reader, false);
                fileItems.Add(fileItem);
            }

            return fileItems;
        }

        public SqlParameter[] ConvertToSqlParameters(FileItem fileItem)
        {
            if (fileItem is null)
            {
                throw new ArgumentNullException(nameof(fileItem));
            }

            SqlParameter[] sqlParameters =
            {
                new SqlParameter()
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = fileItem.Name,
                },
                new SqlParameter()
                {
                    ParameterName = "@fileExtensionId",
                    DbType = DbType.Int32,
                    Value = fileItem.FileExtensionId
                },
                new SqlParameter()
                {
                    ParameterName = "@description",
                    DbType = DbType.String,
                    Value = fileItem.Description,
                },
                new SqlParameter()
                {
                    ParameterName = "@userId",
                    DbType = DbType.Int32,
                    Value = fileItem.UserId,
                },
                new SqlParameter()
                {
                    ParameterName = "@fileCategoryId",
                    DbType = DbType.Int32,
                    Value = fileItem.FileCategoryId,
                }
            };

            return sqlParameters;
        }
    }
}
