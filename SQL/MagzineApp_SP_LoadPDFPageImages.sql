IF OBJECT_ID('LoadPDFPageImages','P') IS NOT NULL
DROP PROC LoadPDFPageImages
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
--exec LoadPDFPageImages @DocumentId = 1

CREATE PROCEDURE LoadPDFPageImages
	@DocumentId as int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DocumentImageId, OriginalDocumentId, DocumentPageNumber, PageCreationDate, PageBinary
	FROM DocumentImage
	WHERE OriginalDocumentId = @DocumentId
END
GO