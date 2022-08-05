

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Generate_Nomor_Dumas]
(	
	-- Add the parameters for the function here
	@TANGGAL DATETIME
)

--SELECT [dbo].[Generate_Nomor_PTSMI](GETDATE())

RETURNS VARCHAR(100) 
AS BEGIN

DECLARE @MONTH INT;
DECLARE @YEAR INT;



SET @MONTH = MONTH(@TANGGAL);
SET @YEAR = YEAR(@TANGGAL);

--DECLARE @TANGGAL DATETIME = GETDATE();
--SELECT TOP 1 SUBSTRING(Nomor, 1, 5) FROM tblT_Dumas WHERE YEAR(CreatedOn) = @YEAR AND MONTH(CreatedOn) = @MONTH ORDER BY SUBSTRING(Nomor, 1, 5) DESC

DECLARE @Nomor_Baru VARCHAR(50);
SET @Nomor_Baru = '00001' + '/PTSMI/' + dbo.fnConvertIntToRoman(@MONTH) + '/' + CONVERT(VARCHAR(4), @YEAR);
DECLARE @COUNT_EXISTING INT, @RUNNING_NUMBER INT;
SET @COUNT_EXISTING = (SELECT TOP 1 SUBSTRING(Nomor, 1, 5) FROM tblT_Dumas WHERE YEAR(CreatedOn) = @YEAR AND MONTH(CreatedOn) = @MONTH ORDER BY SUBSTRING(Nomor, 1, 5) DESC)


IF(@COUNT_EXISTING IS NOT NULL)
BEGIN
	SET @RUNNING_NUMBER = @COUNT_EXISTING + 1;
	SET @Nomor_Baru = RIGHT('00000' + CAST((@RUNNING_NUMBER) AS VARCHAR(5)), 5) + '/PTSMI/' + dbo.fnConvertIntToRoman(@MONTH) + '/' + CONVERT(VARCHAR(4), @YEAR);
END

RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT @Nomor_Baru
)
END