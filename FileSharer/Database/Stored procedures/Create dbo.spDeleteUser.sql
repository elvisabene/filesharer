CREATE PROCEDURE dbo.spDeleteUser
    @id INT
AS
BEGIN
    IF @id IS NULL
        THROW 50000, 'Argument was null!', 1
    ELSE
        DELETE dbo.tUser
        WHERE Id = @id
END