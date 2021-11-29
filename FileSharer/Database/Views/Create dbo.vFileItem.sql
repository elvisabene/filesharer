CREATE VIEW dbo.vFileItem AS
SELECT fi.Id, fi.Name, fi.FileExtensionId, fi.Description, fi.UserId, fi.FileCategoryId
FROM dbo.tFileItem AS fi;
