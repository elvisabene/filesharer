CREATE PROCEDURE dbo.spAddUser
    @name NVARCHAR(50),
    @roleId INT,
    @email NVARCHAR(50),
    @passwordHash NVARCHAR(100)
AS
BEGIN
    INSERT INTO dbo.tUser(Name, RoleId, Email, PasswordHash)
    VALUES(@name, @roleId, @email, @passwordHash)
END
