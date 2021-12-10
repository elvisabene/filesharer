CREATE  PROCEDURE dbo.spIncrementDownloadsCount
    @id INT
AS
BEGIN
    IF @id IS NULL
        THROW 50000, 'Argument was null!', 1
    ELSE
        UPDATE dbo.tFileItem
        SET DownloadsCount = DownloadsCount + 1
        WHERE Id = @id
END