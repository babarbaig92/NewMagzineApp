IF NOT EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'dbo.[ImageSection]') and OBJECTPROPERTY(id, N'IsTable') = 1)
	BEGIN
		CREATE TABLE dbo.[ImageSection]
		(
			SectionId int IDENTITY(1,1) NOT NULL,
			OriginalImageId int,
			ImagePartName varchar(100),
			ImagePartByte varbinary(max),
			CoordX1 int,
			CoordX2 int,
			CoordY1 int,
			CoordY2 int,
			SectionWidth int,
			SectionHeight int

			PRIMARY KEY (SectionId)
		)
	END
GO