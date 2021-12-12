CREATE PROCEDURE dbo.spAddFileCategory
    @name NVARCHAR(50)
AS
BEGIN
    IF @name IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        INSERT INTO dbo.tFileCategory(Name)
        VALUES(@name)
END
