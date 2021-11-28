namespace FileSharer.Common.Constants
{
    public static class DatabaseConstants
    {
        public static class StoredProcedureName
        {
            public const string AddFileItem = "dbo.spAddFileItem";

            public const string DeleteFileItem = "dbo.spDeleteFileItem";

            public const string UpdateFileItem = "dbo.spUpdateFileItem";
        }

        public static class ViewName
        {
            public const string AllFileItems = "dbo.vFileItem";
        }
    }
}
