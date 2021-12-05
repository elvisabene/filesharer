USE FileSharerDb
GO

CREATE VIEW dbo.vFileItem AS
SELECT fi.Id AS Id,
fi.Name AS Name,
fi.FileExtensionId AS ExtensionId,
fi.Description AS Description,
fi.UserId AS UserId,
fi.FileCategoryId AS CategoryId
FROM dbo.tFileItem AS fi;
