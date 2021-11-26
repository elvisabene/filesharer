CREATE PROCEDURE dbo.spAddUser
    @name NVARCHAR(50),
    @roleId INT,
    @email NVARCHAR(50),
    @passwordHash NVARCHAR(100)
AS
BEGIN
    IF @name IS NULL OR @roleId IS NULL
            OR @email IS NULL OR @passwordHash IS NULL
            THROW 50000, 'Arguments was null!', 1
    ELSE
        INSERT INTO dbo.tUser(Name, RoleId, Email, PasswordHash)
        VALUES(@name, @roleId, @email, @passwordHash)
END
