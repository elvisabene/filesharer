CREATE PROCEDURE dbo.spUpdateFileCategrory
    @id INT,
    @name NVARCHAR(50)
AS
BEGIN
    IF @id IS NULL OR @name IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        UPDATE dbo.tFileCategory
        SET Name = @name
        WHERE Id = @id
END
