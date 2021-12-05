using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public class UserConverter : IDataConverter<User>
    {
        public User ConvertToSingleItem(SqlDataReader reader, bool withRead = true)
        {
            if (reader is null)
            {
                throw new ArgumentException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                return null;
            }

            if (withRead)
            {
                reader.Read();
            }

            User user = new User()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                RoleId = (int)reader["RoleId"],
                Email = (string)reader["Email"],
                PasswordHash = (string)reader["PasswordHash"],
            };

            return user;
        }

        public IEnumerable<User> ConvertToItemList(SqlDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
            }

            var users = new List<User>();

            while (reader.Read())
            {
                var user = ConvertToSingleItem(reader, false);
                users.Add(user);
            }

            return users;
        }

        public SqlParameter[] ConvertToSqlParameters(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            SqlParameter[] parameters =
            {
                new SqlParameter()
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = user.Name,
                },
                new SqlParameter()
                {
                    ParameterName = "@roleId",
                    DbType = DbType.Int32,
                    Value = user.RoleId,
                },
                new SqlParameter()
                {
                    ParameterName = "@email",
                    DbType = DbType.String,
                    Value = user.Email,
                },
                new SqlParameter()
                {
                    ParameterName = "@passwordHash",
                    DbType = DbType.String,
                    Value = user.PasswordHash,
                }
            };

            return parameters;
        }
    }
}
