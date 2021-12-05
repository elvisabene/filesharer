namespace FileSharer.Common.Constants
{
    public static class DatabaseConstants
    {
        public static class StoredProcedureName
        {
            public static class ForAdd
            {
                public const string FileCategory = "dbo.spAddFileCategory";

                public const string FileExtension = "dbo.spAddFileExtension";

                public const string FileItem = "dbo.spAddFileItem";

                public const string User = "dbo.spAddUser";
            }

            public static class ForDelete
            {
                public const string FileCategory = "dbo.spDeleteFileCategory";

                public const string FileExtension = "dbo.spDeleteFileExtension";

                public const string FileItem = "dbo.spDeleteFileItem";

                public const string User = "dbo.spDeleteUser";
            }

            public static class ForUpdate
            {
                public const string FileCategory = "dbo.spUpdateFileCategory";

                public const string FileExtension = "dbo.spUpdateFileExtension";

                public const string FileItem = "dbo.spUpdateFileItem";

                public const string User = "dbo.spUpdateUser";
            }
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
