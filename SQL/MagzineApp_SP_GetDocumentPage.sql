IF OBJECT_ID('GetDocumentPage','P') IS NOT NULL
DROP PROC GetDocumentPage
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
CREATE PROCEDURE GetDocumentPage
	@DocumentId as int = 0,
	@PageNumber as int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DocumentImageId,PageBinary
	FROM DocumentImage 
	WHERE OriginalDocumentId = @DocumentId AND DocumentPageNumber = @PageNumber
END
GO