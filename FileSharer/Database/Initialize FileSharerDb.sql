USE [master]
CREATE DATABASE FileSharerDb

GO

USE FileSharerDb
CREATE TABLE dbo.tFileCategory
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

GO

USE FileSharerDb
CREATE TABLE dbo.tFileExtension
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

GO

USE FileSharerDb
CREATE TABLE dbo.tRole
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

GO

CREATE TABLE dbo.tUser 
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    RoleId INT REFERENCES dbo.tRole(Id),
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(100)
);

GO

USE FileSharerDb
CREATE TABLE dbo.tFileItem
(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    FileExtensionId INT REFERENCES dbo.tFileExtension(Id),
    Description NVARCHAR(200),
    UserId INT REFERENCES dbo.tUser(Id),
    FileCategoryId INT REFERENCES dbo.tFileCategory(Id),
);

GO

USE FileSharerDb
INSERT INTO dbo.tFileCategory
VALUES
('Image'),
('Document'),
('Video'),
('Music'),
('Archive'),
('Other');

GO

USE FileSharerDb
INSERT INTO dbo.tFileExtension
VALUES
('.jpg'), ('.jpeg'), ('.png'), ('.gif'),
('.pdf'), ('.doc'), ('.xls'), ('.ppt'),
('.mp3'), ('.wav'), ('.mp4'), ('.avi'),
('.txt'), ('.rar'), ('.zip'), ('.7z');

GO

USE FileSharerDb
INSERT INTO dbo.tRole
VALUES ('User'), ('Editor'), ('Admin');

GO

USE FileSharerDb
INSERT INTO dbo.tUser VALUES
('Phil', 1, 'phil@phil.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ='),
('Alex', 2, 'alex@alex.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ='),
('Sam', 3, 'sam@sam.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=');

GO

USE FileSharerDb
INSERT INTO dbo.tFileItem VALUES
('MyDoc', 6, NULL, 1, 2),
('SomeImage', 1, NULL, 1, 1),
('SomeVideo', 11, NULL, 2, 3),
('SomePresentation', 8, NULL, 2, 2),
('MyZip', 15, NULL, 3, 5),
('MyText', 13, NULL, 3, 6);