using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public static class FileItemConverter
    {
        public static FileItem ConvertToFileItem(this SqlDataReader dataReader)
        {
            if (dataReader is null)
            {
                throw new ArgumentNullException(nameof(dataReader));
            }

            if (!dataReader.HasRows)
            {
                throw new ArgumentException();
            }

            dataReader.Read();

            FileItem fileItem = new FileItem()
            {
                Id = (int)dataReader["Id"],
                Name = (string)dataReader["Name"],
                FileExtensionId = (int)dataReader["FileExtensionId"],
                Description = (string)dataReader["Description"] ?? string.Empty,
                UserId = (int)dataReader["UserId"],
                FileCategoryId = (int)dataReader["UserId"],
            };

            return fileItem;
        }

        public static IEnumerable<FileItem> ConvertToFileItemList(this SqlDataReader dataReader)
        {
            if (dataReader is null)
            {
                throw new ArgumentNullException(nameof(dataReader));
            }

            if (!dataReader.HasRows)
            {
                throw new ArgumentException();
            }

            var fileItems = new List<FileItem>();

            while (dataReader.Read())
            {
                var fileItem = dataReader.ConvertToFileItem();
                fileItems.Add(fileItem);
            }

            return fileItems;
        }

        public static SqlParameter[] ConvertToSqlParameters(this FileItem fileItem)
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
