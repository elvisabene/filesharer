CREATE VIEW dbo.vGetAllUsers AS
SELECT u.Id, u.Name, u.RoleId, u.Email, u.PasswordHash
FROM dbo.tUser AS u;
