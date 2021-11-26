CREATE PROCEDURE dbo.spUpdateUser
    @id INT,
    @name NVARCHAR(50),
    @roleId INT,
    @email NVARCHAR(50),
    @passwordHash NVARCHAR(100)
AS
BEGIN
    IF @id IS NULL OR @name IS NULL
        OR @roleId IS NULL OR @email IS NULL
        OR @passwordHash IS NULL
        THROW 50000, 'Arguments was null!', 1
    ELSE
        UPDATE dbo.tUser
        SET Name = @name,
            RoleId = @roleId,
            Email = @email,
            PassworHash = @passwordHash
        WHERE Id = @id
END
