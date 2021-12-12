CREATE PROCEDURE dbo.spDeleteFileExtension
    @id INT
AS
BEGIN
    IF @id IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        DELETE dbo.tFileExtension
        WHERE Id = @id
END
