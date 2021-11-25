﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileSharer.Data.Database
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; }

        public DatabaseSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
