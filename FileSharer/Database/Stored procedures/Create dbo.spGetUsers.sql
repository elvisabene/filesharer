USE FileSharerDb;

GO

CREATE PROCEDURE dbo.spGetUsers AS
BEGIN
    SELECT * FROM dbo.tUser
END;