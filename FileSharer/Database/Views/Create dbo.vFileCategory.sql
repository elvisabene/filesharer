CREATE VIEW dbo.vFileCategory AS
SELECT ft.Id AS Id,
ft.Name AS Name
FROM dbo.tFileCategory AS ft;
