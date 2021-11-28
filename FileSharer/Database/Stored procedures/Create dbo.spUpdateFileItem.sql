CREATE PROCEDURE dbo.spUpdateFileItem
    @id INT,
    @name NVARCHAR(100),
    @fileExtensionId INT,
    @description NVARCHAR(200),
    @userId INT,
    @fileCategoryId INT
AS
BEGIN
    IF @id IS NULL OR @name IS NULL
        OR NOT EXISTS(SELECT * FROM dbo.tFileExtension WHERE Id = @fileExtensionId)
        OR NOT EXISTS(SELECT * FROM dbo.tUser WHERE Id = @userId)
        OR NOT EXISTS(SELECT * FROM dbo.tFileCategory WHERE Id = @categoryId)
        OR @fileExtensionId IS NULL OR @userId IS NULL OR @fileCategoryId IS NULL
            THROW 50000, 'Arguments was null or not existing!', 1
    ELSE
        UPDATE dbo.tFileItem
        SET Name = @name,
            FileExtensionId = @fileExtensionId,
            Description = @description,
            UserId = @userId,
            FileCategoryId = @fileCategoryId
        WHERE Id = @id
END
