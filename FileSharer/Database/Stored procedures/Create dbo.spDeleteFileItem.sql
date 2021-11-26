CREATE PROCEDURE dbo.spDeleteFileItem
    @id INT
AS
BEGIN
    IF @id IS NULL
        THROW 50000, 'Argument was null!', 1
    ELSE
        DELETE dbo.tFileItem
        WHERE Id = @id
END