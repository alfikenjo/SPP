







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Year_Dumas]	
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @StartYear INT = (SELECT TOP 1 YEAR(CreatedOn) FROM tblT_Dumas WHERE Nomor IS NOT NULL ORDER BY CreatedOn ASC)
	DECLARE @EndYear INT = (SELECT TOP 1 YEAR(CreatedOn) FROM tblT_Dumas WHERE Nomor IS NOT NULL ORDER BY CreatedOn DESC)

	DECLARE @StartMonth INT = (SELECT TOP 1 Month(CreatedOn) FROM tblT_Dumas WHERE Nomor IS NOT NULL ORDER BY CreatedOn ASC)
	DECLARE @EndMonth INT = (SELECT TOP 1 Month(CreatedOn) FROM tblT_Dumas WHERE Nomor IS NOT NULL ORDER BY CreatedOn DESC)

	IF(@StartYear IS NULL) BEGIN SET @StartYear = YEAR(GETDATE()) END
	IF(@EndYear IS NULL) BEGIN SET @EndYear = YEAR(GETDATE()) END

	IF(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL) > 0
	BEGIN
		SELECT	DISTINCT YEAR(CreatedOn) [Year], @StartMonth [StartMonth], @EndMonth [EndMonth]
		FROM	tblT_Dumas A		
		WHERE	YEAR(CreatedOn) BETWEEN @StartYear AND @EndYear
	END

	ELSE 
	BEGIN
		SELECT @StartYear [Year], MONTH(GETDATE()) [StartMonth], MONTH(GETDATE()) [EndMonth]
	END

END