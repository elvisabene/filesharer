CREATE PROCEDURE dbo.spAddFileItem
    @name NVARCHAR(100),
    @fileExtensionId INT,
    @description NVARCHAR(200),
    @userId INT,
    @fileCategoryId INT
AS
BEGIN
    INSERT INTO dbo.tFileItem(Name, FileExtensionId, Description, UserId, FileCategoryId)
    VALUES(@name, @fileExtensionId, @description, @userId, @fileCategoryId)
END
