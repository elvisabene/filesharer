using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public class FileCategoryConverter : IDataConverter<FileCategory>
    {
        public IEnumerable<FileCategory> ConvertToItemList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
            }

            var fileCategories = new List<FileCategory>();

            while (reader.Read())
            {
                var fileCategory = ConvertToSingleItem(reader, false);
                fileCategories.Add(fileCategory);
            }

            return fileCategories;
        }

        public FileCategory ConvertToSingleItem(SqlDataReader reader, bool withRead = true)
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

            var fileCategory = new FileCategory()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
            };

            return fileCategory;
        }

        public SqlParameter[] ConvertToSqlParameters(FileCategory item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            SqlParameter[] parameters =
            {
                new SqlParameter()
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = item.Name,
                }
            };

            return parameters;
        }
    }
}
