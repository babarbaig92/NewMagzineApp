IF NOT EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[Document]') and OBJECTPROPERTY(id, N'IsTable') = 1)
	BEGIN
		CREATE TABLE dbo.[Document]
		(
			DocumentId int IDENTITY(1,1) NOT NULL,
			DocumentName nvarchar(200),
			DocumentType nvarchar(100),
			DocumentLength nvarchar(200),
			UploadDate DateTime,
			DocumentContent varbinary (max),
			HasGeneratedImages bit,
			PRIMARY KEY (DocumentId)
		)
	END
GO