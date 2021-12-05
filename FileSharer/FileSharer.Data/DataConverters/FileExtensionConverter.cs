using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public class FileExtensionConverter : IDataConverter<FileExtension>
    {
        public IEnumerable<FileExtension> ConvertToItemList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
            }

            var fileCategories = new List<FileExtension>();

            while (reader.Read())
            {
                var fileCategory = ConvertToSingleItem(reader, false);
                fileCategories.Add(fileCategory);
            }

            return fileCategories;
        }

        public FileExtension ConvertToSingleItem(SqlDataReader reader, bool withRead = true)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
            }

            if (withRead)
            {
                reader.Read();
            }

            var fileCategory = new FileExtension()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
            };

            return fileCategory;
        }

        public SqlParameter[] ConvertToSqlParameters(FileExtension item)
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
                    Value = item.Name,
                }
            };

            return parameters;
        }
    }
}
