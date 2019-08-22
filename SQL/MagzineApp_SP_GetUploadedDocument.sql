IF OBJECT_ID('GetUploadedDocuments','P') IS NOT NULL
DROP PROC GetUploadedDocuments
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
--exec GetUploadedDocuments @FromDate='2019-07-27 13:37:54.980',@ToDate='2019-07-27 13:43:54.980'
--exec GetUploadedDocuments @FromDate='2019-07-27',@ToDate=NULL
--exec GetUploadedDocuments @FromDate=NULL,@ToDate='2019-07-27 13:43:54.980'
--exec GetUploadedDocuments @FromDate=NULL,@ToDate=NULL
--exec GetUploadedDocuments @FromDate='2019-07-31',@ToDate='2019-08-02'
--exec GetUploadedDocuments @FromDate='07/31/2019',@ToDate='08/02/2019'


CREATE PROCEDURE GetUploadedDocuments
	@FromDate as varchar(200) = '',
	@ToDate as varchar(200) = ''
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @sql nvarchar(4000) = ''
	DECLARE @select nvarchar(2000) = ''
	DECLARE @where nvarchar(2000) = ''
	DECLARE @from nvarchar(2000) = ''
	DECLARE @bitAnd bit = 0
	SELECT @select = N'SELECT DocumentId, DocumentName, UploadDate, HasGeneratedImages '
	SELECT @from = N' FROM Document'

	IF @FromDate <> ''
	BEGIN
		SELECT @where = N' WHERE UploadDate >= ''' + @FromDate + N''''
		SET @bitAnd = 1
	END
	IF @ToDate <> ''
	BEGIN
		IF @bitAnd = 1
		BEGIN
			SELECT @where += ' AND '
		END
		ELSE
		BEGIN
			SELECT @where = ' WHERE'
		END
		SELECT @where += ' UploadDate <= ''' + @ToDate + N''''
	END
	SELECT @sql = @select + @from + @where
	print(@sql)
	EXECUTE sp_ExecuteSQL @sql, N''

END
GO