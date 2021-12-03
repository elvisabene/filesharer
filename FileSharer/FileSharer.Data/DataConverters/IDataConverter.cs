using System.Collections.Generic;
using System.Data.SqlClient;

namespace FileSharer.Data.DataConverters
{
    public interface IDataConverter<T>
    {
        T ConvertToSingleItem(SqlDataReader reader, bool withRead = true);

        IEnumerable<T> ConvertToItemList(SqlDataReader reader);

        SqlParameter[] ConvertToSqlParameters(T item);
    }
}
