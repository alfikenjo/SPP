






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_FileEkstensionFilter]
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), ID) [ID]
			,Name
			,ISNULL(UpdatedBy, CreatedBy) [UpdatedBy]
			,dbo.Format24DateTime(ISNULL(UpdatedOn, CreatedOn)) [UpdatedOn]	
	FROM	FileEkstensionFilter
	ORDER BY Name
END