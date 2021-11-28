namespace FileSharer.Common.Constants
{
    public static class DatabaseConstants
    {
        public static class StoredProcedureName
        {
            public const string AddFileItem = "dbo.spAddFileItem";

            public const string DeleteFileItem = "dbo.spDeleteFileItem";
        }

        public static class ViewName
        {
            public const string AllFileItems = "dbo.vFileItem";
        }
    }
}
