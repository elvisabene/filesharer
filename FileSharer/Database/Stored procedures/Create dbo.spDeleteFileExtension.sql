CREATE PROCEDURE dbo.spDeleteFileExtension
    @id INT,
    @name NVARCHAR(50)
AS
BEGIN
    IF @id IS NULL OR @name IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        DELETE dbo.tFileExtension
        WHERE Id = @id
END
