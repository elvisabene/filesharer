using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public class RoleConverter : IDataConverter<Role>
    {
        public IEnumerable<Role> ConvertToItemList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                return null;
            }

            var roles = new List<Role>();

            while (reader.Read())
            {
                var role = ConvertToSingleItem(reader, false);
                roles.Add(role);
            }

            return roles;
        }

        public Role ConvertToSingleItem(SqlDataReader reader, bool withRead = true)
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

            var role = new Role()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
            };

            return role;
        }

        public SqlParameter[] ConvertToSqlParameters(Role item)
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
