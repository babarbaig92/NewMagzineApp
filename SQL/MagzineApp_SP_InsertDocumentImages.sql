IF OBJECT_ID('InsertDocumentImages','P') IS NOT NULL
DROP PROC InsertDocumentImages
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec InsertDocumentImages @DocumentName = 'aa'
-- =============================================
CREATE PROCEDURE InsertDocumentImages
	@OriginalDocumentId as int = 0,
	@DocumentPageNumber as int = 0,
	@PageBinary as varbinary (max) = NULL,
	
	@DocumentImageId int = 0 OUTPUT
AS
BEGIN
	INSERT INTO DocumentImage(OriginalDocumentId, DocumentPageNumber, PageCreationDate ,PageBinary)
	VALUES (@OriginalDocumentId, @DocumentPageNumber, GETDATE(), @PageBinary)

	SELECT @DocumentImageId = SCOPE_IDENTITY()
END
GO