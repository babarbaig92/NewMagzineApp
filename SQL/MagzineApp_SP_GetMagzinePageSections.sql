IF OBJECT_ID('GetMagzinePageSections','P') IS NOT NULL
DROP PROC GetMagzinePageSections
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec GetImageSection @DocumentId = 2
CREATE PROCEDURE GetMagzinePageSections
	@pageId as int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM ImageSection
	WHERE OriginalImageId = @pageId
END
GO