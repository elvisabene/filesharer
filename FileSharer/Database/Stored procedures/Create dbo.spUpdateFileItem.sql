CREATE PROCEDURE dbo.spUpdateFileItem
    @id INT,
    @name NVARCHAR(100),
    @fileExtensionId INT,
    @description NVARCHAR(200),
    @userId INT,
    @fileCategoryId INT
AS
BEGIN
    IF @id IS NULL OR @name IS NULL OR @fileExtensionId IS NULL
        OR @userId IS NULL OR @fileCategoryId IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        UPDATE dbo.tFileItem
        SET Name = @name,
            FileExtensionId = @fileExtensionId,
            Description = @description,
            UserId = @userId,
            FileCategoryId = @fileCategoryId
        WHERE Id = @id
END
