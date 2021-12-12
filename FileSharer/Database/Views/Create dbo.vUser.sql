CREATE VIEW dbo.vUser AS
SELECT u.Id AS Id,
u.Name AS Name,
u.RoleId AS RoleId,
u.Email AS Email,
u.PasswordHash AS PasswordHash
FROM dbo.tUser AS u;
