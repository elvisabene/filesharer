using FileSharer.Data.DataConverters;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FileSharer.Data.Infrastructure
{
    public interface IDataContext
    {
        void ExecuteNonQuery(SqlCommand command);

        T ExecuteQueryAsSingle<T>(SqlCommand command, IDataConverter<T> converter);

        IEnumerable<T> ExecuteQueryAsList<T>(SqlCommand command, IDataConverter<T> converter);
    }
}
