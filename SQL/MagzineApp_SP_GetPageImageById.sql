IF OBJECT_ID('GetPageImageById','P') IS NOT NULL
DROP PROC GetPageImageById
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
CREATE PROCEDURE GetPageImageById
	@PageImageId as int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PageBinary
	FROM DocumentImage 
	WHERE DocumentImageId = @PageImageId
END
GO