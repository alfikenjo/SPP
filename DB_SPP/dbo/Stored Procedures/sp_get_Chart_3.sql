




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_Chart_3]
	@StartYear VARCHAR(4),
	@EndYear  VARCHAR(4)
AS
BEGIN

	--DECLARE @StartYear VARCHAR(4) = 2001,
	--@EndYear  VARCHAR(4) = 2022

	DECLARE @TABLE AS TABLE (Tahun INT, Kategori VARCHAR(200), Kategori_EN VARCHAR(200), Jumlah INT)

	INSERT INTO @TABLE
	SELECT	YEAR(A.CreatedOn) [Tahun],
			ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown') [Jenis_Pelanggaran],
			ISNULL(NULLIF(dbo.ProperCase((SELECT TOP 1 GridTitle FROM tblT_CMS WHERE Lang = 'EN' AND ID_IN = B.ID)), ''), 'Unknown') [Jenis_Pelanggaran_EN],
			COUNT(A.ID) [Jumlah]
	FROM	tblT_Dumas A
			LEFT JOIN tblT_CMS B ON B.Tipe = 'Jenis Pelanggaran' AND B.Lang = 'ID' AND ISNULL(B.GridTitle, 'N#A') = A.Jenis_Pelanggaran
	WHERE	A.Nomor IS NOT NULL
			AND YEAR(A.CreatedOn) BETWEEN @StartYear AND @endYear	
	GROUP BY YEAR(A.CreatedOn), ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown'), B.ID


	DECLARE @TABLE_TAHUN AS TABLE (Tahun INT)
	INSERT INTO @TABLE_TAHUN SELECT DISTINCT Tahun FROM @TABLE

	DECLARE @TABLE_Kategori AS TABLE (Kategori VARCHAR(200), Kategori_EN VARCHAR(200))
	INSERT INTO @TABLE_Kategori SELECT DISTINCT Kategori, Kategori_EN FROM @TABLE

	
	SELECT	CONVERT(VARCHAR(4), A.Tahun) [Tahun], A.Kategori, A.Kategori_EN, ISNULL(B.Jumlah, 0) [Jumlah]
	FROM	(SELECT	*  FROM	@TABLE_TAHUN, @TABLE_Kategori) A
			LEFT JOIN @TABLE B ON A.Tahun = B.Tahun AND A.Kategori = B.Kategori
	
	
	
END