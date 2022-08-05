




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_DetailChart_3]
	@StartYear INT,
	@EndYear INT,
	@StartMonth INT,
	@EndMonth INT,
	@Lang VARCHAR(2) = 'ID'
AS
BEGIN

	--DECLARE @StartYear VARCHAR(4) = 2001,
	--@EndYear  VARCHAR(4) = 2022,
	--@StartMonth INT = 1,
	--@EndMonth INT = 7,
	--@Lang VARCHAR(2) = 'en';

	DECLARE @Start VARCHAR(6) = (SELECT CONVERT(VARCHAR(4), @StartYear) + REPLICATE('0',2-LEN(RTRIM(@StartMonth))) + RTRIM(@StartMonth))
	DECLARE @End VARCHAR(6) = (SELECT CONVERT(VARCHAR(4), @EndYear) + REPLICATE('0',2-LEN(RTRIM(@EndMonth))) + RTRIM(@EndMonth))	

	DECLARE @TABLE AS TABLE (Kategori VARCHAR(200), Jumlah INT)

	INSERT INTO @TABLE
	SELECT	--YEAR(A.CreatedOn) [Tahun],
			ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown') [Jenis_Pelanggaran],			
			COUNT(A.ID) [Jumlah]
	FROM	tblT_Dumas A
			LEFT JOIN tblT_CMS B ON B.Tipe = 'Jenis Pelanggaran' AND B.Lang = @Lang AND ISNULL(B.GridTitle, 'N#A') = A.Jenis_Pelanggaran
	WHERE	A.Nomor IS NOT NULL
			AND dbo.FormatDate_yyyyMM(A.CreatedOn) BETWEEN @Start AND @End
	GROUP BY ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown')

	IF(@Lang = 'en')
	BEGIN
		UPDATE	A SET A.Kategori = (SELECT dbo.ProperCase(GridTitle) FROM tblT_CMS WHERE ID_IN = B.ID)
		FROM	@TABLE A
				JOIN tblT_CMS B ON A.Kategori = B.GridTitle AND B.Tipe = 'Jenis Pelanggaran' AND B.Lang = 'ID'
	END

	DECLARE @TABLE_Kategori AS TABLE (Kategori VARCHAR(200))
	INSERT INTO @TABLE_Kategori SELECT DISTINCT dbo.ProperCase(GridTitle) FROM tblT_CMS WHERE TIpe = 'Jenis Pelanggaran' AND Lang = @Lang

	
	SELECT	A.Kategori, LTRIM(RTRIM(REPLACE(A.Kategori, ' ', ''))) [Kategori_EN], ISNULL(B.Jumlah, 0) [Jumlah]
	FROM	@TABLE_Kategori A
			LEFT JOIN @TABLE B ON A.Kategori = B.Kategori
	
	
	
END