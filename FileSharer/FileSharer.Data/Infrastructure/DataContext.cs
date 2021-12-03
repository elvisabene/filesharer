using FileSharer.Data.Database;
using FileSharer.Data.DataConverters;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public T ExecuteQueryAsSingle<T>(SqlCommand command, IDataConverter<T> converter)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                T item = converter.ConvertToSingleItem(reader);

                return item;
            }
        }

        public IEnumerable<T> ExecuteQueryAsList<T>(SqlCommand command, IDataConverter<T> converter)
        {
            using (SqlConnection connection = new SqlConnection(_dbSettings.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IEnumerable<T> items = converter.ConvertToItemList(reader);

                return items;
            }
        }
    }
}
