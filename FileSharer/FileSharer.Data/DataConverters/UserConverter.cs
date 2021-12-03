using FileSharer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public static class UserConverter
    {
        public static User ConvertToUser(this SqlDataReader reader, bool withRead = true)
        {
            if (reader is null)
            {
                throw new ArgumentException(nameof(reader));
            }

            if (!reader.HasRows)
            {
                throw new ArgumentException();
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

        public static IEnumerable<User> ConvertToUserList(this SqlDataReader reader)
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
                var user = reader.ConvertToUser(false);
                users.Add(user);
            }

            return users;
        }

        public static SqlParameter[] ConvertToSqlParameters(this User user)
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
