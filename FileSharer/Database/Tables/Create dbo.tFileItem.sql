CREATE TABLE dbo.tFileItem 
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    FileExtensionId INT REFERENCES dbo.tFileExtension(Id),
    Description NVARCHAR(200),
    UserId INT REFERENCES dbo.tUser(Id),
    FileCategoryId INT REFERENCES dbo.tFileCategory(Id),
);