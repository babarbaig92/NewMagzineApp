IF OBJECT_ID('InsertDocument','P') IS NOT NULL
DROP PROC InsertDocument
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec InsertDocument @DocumentName = 'aa'
-- =============================================
CREATE PROCEDURE InsertDocument
	@DocumentName as varchar(200) = '',
	@DocumentType as varchar(100) = '',
	@DocumentLength as varchar (200) = '',
	@DocumentContent as varbinary(max) = NULL,
	@DocumentId int = 0 OUTPUT
AS
BEGIN
	INSERT INTO Document(DocumentName, DocumentType, DocumentLength, DocumentContent, UploadDate)
	VALUES (@DocumentName, @DocumentType, @DocumentLength, @DocumentContent, GETDATE())

	SELECT @DocumentId = SCOPE_IDENTITY()
END
GO