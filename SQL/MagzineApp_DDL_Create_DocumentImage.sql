IF NOT EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[DocumentImage]') and OBJECTPROPERTY(id, N'IsTable') = 1)
	BEGIN
		CREATE TABLE dbo.[DocumentImage]
		(
			DocumentImageId int IDENTITY(1,1) NOT NULL,
			OriginalDocumentId int,
			DocumentPageNumber int,
			PageCreationDate DateTime,
			PageBinary varbinary(max)
			PRIMARY KEY (DocumentImageId)
		)
	END
GO