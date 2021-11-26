CREATE VIEW dbo.vGetAllFileCategories AS
SELECT ft.Id, ft.Name
FROM dbo.tFileCategory AS ft;
