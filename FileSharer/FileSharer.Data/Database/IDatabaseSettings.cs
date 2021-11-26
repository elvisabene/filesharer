using System;
using System.Collections.Generic;
using System.Text;

namespace FileSharer.Data.Database
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }
    }
}
