using FileSharer.Data.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FileSharer.Data.Infrastructure
{
    public class DataContext : IDataContext
    {
        private IDatabaseSettings _dbSettings;

        public DataContext(IDatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        public void ExecuteNonQuery(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public SqlDataReader ExecuteQuery(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                return reader;
            }
        }
    }
}
