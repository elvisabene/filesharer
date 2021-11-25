CREATE TABLE dbo.tFileItem 
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    FileExtensionId INT REFERENCES dbo.tFileExtension(Id) NOT NULL,
    Description NVARCHAR(200) NULL,
    UserId INT REFERENCES dbo.tUser(Id) NOT NULL,
    FileCategoryId INT REFERENCES dbo.tFileCategory(Id) NOT NULL,
);
