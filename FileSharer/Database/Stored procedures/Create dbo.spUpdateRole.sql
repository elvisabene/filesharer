CREATE PROCEDURE dbo.spUpdateRole
    @id INT,
    @name NVARCHAR(50)
AS
BEGIN
    IF @id IS NULL OR @name IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        UPDATE dbo.tRole
        SET Name = @name
        WHERE Id = @id
END
