USE FileSharerDb

GO

CREATE PROCEDURE dbo.spGetFilesByUserId @userId  INT AS
BEGIN
    SELECT * FROM dbo.tFileItem
    WHERE dbo.tFileItem.UserId = @userId
END;