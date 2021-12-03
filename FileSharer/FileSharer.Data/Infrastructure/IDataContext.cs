using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FileSharer.Data.Infrastructure
{
    public interface IDataContext
    {
        void ExecuteNonQuery(SqlCommand command);

        SqlDataReader ExecuteQuery(SqlCommand command);
    }
}
