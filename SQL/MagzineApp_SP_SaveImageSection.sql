IF OBJECT_ID('SaveImageSection','P') IS NOT NULL
DROP PROC SaveImageSection
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec SaveImageSection @DocumentName = 'aa'
-- =============================================
CREATE PROCEDURE SaveImageSection
	@OriginalImageId as int = 0,
	@ImagePartName as varchar(100) = '',
	@ImagePartByte  varbinary(max) = NULL,
	@CoordX1 as int = 0,
	@CoordX2 as int = 0,
	@CoordY1 as int = 0,
	@CoordY2 as int = 0,
	@Width as int = 0,
	@Height as int = 0,

	@ImageSectionId int = 0 OUTPUT
AS
BEGIN
	INSERT INTO ImageSection(OriginalImageId, ImagePartName, ImagePartByte, CoordX1, CoordX2, CoordY1, CoordY2, SectionWidth, SectionHeight)
	VALUES (@OriginalImageId, @ImagePartName, @ImagePartByte, @CoordX1, @CoordX2, @CoordY1, @CoordY2, @Width, @Height)

	SELECT @ImageSectionId = SCOPE_IDENTITY()
END
GO