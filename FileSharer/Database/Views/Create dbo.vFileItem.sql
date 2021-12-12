CREATE VIEW dbo.vFileItem AS
SELECT fi.Id AS Id,
fi.Name AS Name,
fi.FileExtensionId AS ExtensionId,
fi.Description AS Description,
fi.CreateDate AS CreateDate,
fi.Size AS Size,
fi.DownloadsCount AS DownloadsCount,
fi.UserId AS UserId,
fi.FileCategoryId AS CategoryId
FROM dbo.tFileItem AS fi;
