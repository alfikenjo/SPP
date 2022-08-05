CREATE PROCEDURE [dbo].[sp_Get_Pengaduan_Status_by_Period]
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
	

	DECLARE @TABLE AS TABLE (Total INT, BelumDiproses INT, Diproses INT, Selesai INT, Ditolak INT)

	INSERT INTO @TABLE VALUES (0,0,0,0,0)

	UPDATE @TABLE SET Total = (SELECT COUNT(*) [Total] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End);
	UPDATE @TABLE SET BelumDiproses = (SELECT COUNT(*) [BelumDiproses] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND [Status] IN ('Terkirim'));
	UPDATE @TABLE SET Diproses = (SELECT COUNT(*) [Diproses] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND [Status] IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut'));
	UPDATE @TABLE SET Selesai = (SELECT COUNT(*) [Selesai] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND [Status] = 'Selesai') 
	UPDATE @TABLE SET Ditolak = (SELECT COUNT(*) [Ditolak] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND dbo.FormatDate_yyyyMM(CreatedOn) BETWEEN @Start AND @End  AND [Status] = 'Ditolak Admin SPP') 

	SELECT * FROM @TABLE
	
END