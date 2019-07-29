IF OBJECT_ID('GetDocumentBinary','P') IS NOT NULL
DROP PROC GetDocumentBinary
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
--exec GetDocumentBinary @DocumentId = 2
CREATE PROCEDURE GetDocumentBinary
	@DocumentId as int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DocumentContent 
	FROM Document 
	WHERE DocumentId = @DocumentId
END
GO