namespace FileSharer.Data.Database
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; }

        public DatabaseSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
