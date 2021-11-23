CREATE TABLE dbo.tUser 
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    RoleId INT REFERENCES dbo.tRole(Id),
    Email NVARCHAR(100),
    Password NVARCHAR(100)
);
