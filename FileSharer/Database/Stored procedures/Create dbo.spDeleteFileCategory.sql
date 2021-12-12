CREATE PROCEDURE dbo.spDeleteFileCategory
    @id INT
AS
BEGIN
    IF @id IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        DELETE dbo.tFileCategory
        WHERE Id = @id
END
