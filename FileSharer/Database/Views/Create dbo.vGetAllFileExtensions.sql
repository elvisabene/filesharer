CREATE VIEW dbo.vGetAllFileExtensions AS
SELECT fe.Id, fe.Name
FROM dbo.tFileExtension AS fe;
