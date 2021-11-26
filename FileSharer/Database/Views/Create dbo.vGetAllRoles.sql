CREATE VIEW dbo.vGetAllRoles AS
SELECT r.Id, r.Name
FROM dbo.tRole AS r;
