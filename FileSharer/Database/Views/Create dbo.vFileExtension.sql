CREATE VIEW dbo.vFileExtension AS
SELECT fe.Id AS Id,
fe.Name AS Name
FROM dbo.tFileExtension AS fe;
