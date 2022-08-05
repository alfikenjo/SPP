CREATE PROCEDURE [dbo].[sp_get_DetailChart_2]
	@StartYear INT,
	@EndYear INT,
	@StartMonth INT,
	@EndMonth INT
AS
BEGIN

	--DECLARE @StartYear INT = 2020,
	--		@EndYear INT = 2022,
	--		@StartMonth INT = 1,
	--		@EndMonth INT = 7;

	
	DECLARE @Start VARCHAR(6) = (SELECT CONVERT(VARCHAR(4), @StartYear) + REPLICATE('0',2-LEN(RTRIM(@StartMonth))) + RTRIM(@StartMonth))
	DECLARE @End VARCHAR(6) = (SELECT CONVERT(VARCHAR(4), @EndYear) + REPLICATE('0',2-LEN(RTRIM(@EndMonth))) + RTRIM(@EndMonth))
	

	DECLARE @TABLE AS TABLE (Portal INT, Email INT, Surat INT, Telepon INT, Fax INT)

	INSERT INTO @TABLE VALUES (0,0,0,0,0)

	UPDATE @TABLE SET Portal = (SELECT COUNT(*) [Portal] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End AND ISNULL(NULLIF(Sumber, ''), 'Portal') = 'Portal SPP');
	UPDATE @TABLE SET Email = (SELECT COUNT(*) [Email] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND (Sumber = 'Email'));
	UPDATE @TABLE SET Surat = (SELECT COUNT(*) [Surat] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND (Sumber = 'Surat'));
	UPDATE @TABLE SET Telepon = (SELECT COUNT(*) [Telepon] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND (Sumber = 'Telepon')) 
	UPDATE @TABLE SET Fax = (SELECT COUNT(*) [Fax] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND (Sumber = 'Fax')) 

	SELECT * FROM @TABLE
	
END


--SELECT Sumber, COunt(*) FROM tblT_Dumas GROUP BY Sumber