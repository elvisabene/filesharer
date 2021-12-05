namespace FileSharer.Common.Constants
{
    public static class DatabaseConstants
    {
        public static class StoredProcedureName
        {
            public const string AddFileCategory = "dbo.spAddFileCategory";

            public const string AddFileItem = "dbo.spAddFileItem";

            public const string AddUser = "dbo.spAddUser";

            public const string DeleteFileCategory = "dbo.spDeleteFileCategory";

            public const string DeleteFileItem = "dbo.spDeleteFileItem";

            public const string DeleteUser = "dbo.spDeleteUser";

            public const string UpdateFileCategory = "dbo.spUpdateFileCategory";

            public const string UpdateFileItem = "dbo.spUpdateFileItem";

            public const string UpdateUser = "dbo.spUpdateUser";
        }

        public static class ViewName
        {
            public const string AllFileCategories = "dbo.vFileCategiry";

            public const string AllFileExtensions = "dbo.vFileExtension";

            public const string AllFileItems = "dbo.vFileItem";

            public const string AllRoles = "dbo.vRole";

            public const string AllUsers = "dbo.vUser";
        }
    }
}
