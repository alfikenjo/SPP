




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_Chart_3_bulanan]
	@StartYear VARCHAR(4)
AS
BEGIN

	--DECLARE @StartYear VARCHAR(4) = 2022

	DECLARE @TABLE AS TABLE (Bulan INT, Kategori VARCHAR(200), Kategori_EN VARCHAR(200), Jumlah INT)

	INSERT INTO @TABLE
	SELECT	MONTH(A.CreatedOn) [Tahun],
			ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown') [Jenis_Pelanggaran],
			ISNULL(NULLIF(dbo.ProperCase((SELECT TOP 1 GridTitle FROM tblT_CMS WHERE Lang = 'EN' AND ID_IN = B.ID)), ''), 'Unknown') [Jenis_Pelanggaran_EN],
			COUNT(A.ID) [Jumlah]
	FROM	tblT_Dumas A
			LEFT JOIN tblT_CMS B ON B.Tipe = 'Jenis Pelanggaran' AND B.Lang = 'ID' AND ISNULL(B.GridTitle, 'N#A') = A.Jenis_Pelanggaran
	WHERE	A.Nomor IS NOT NULL
			AND YEAR(A.CreatedOn) = @StartYear
	GROUP BY Month(A.CreatedOn), ISNULL(NULLIF(dbo.ProperCase(A.Jenis_Pelanggaran), ''), 'Unknown'), B.ID

	DECLARE @TABLE_Kategori AS TABLE (Kategori VARCHAR(200), Kategori_EN VARCHAR(200))
	INSERT INTO @TABLE_Kategori SELECT DISTINCT Kategori, Kategori_EN FROM @TABLE

	
	SELECT	CONVERT(VARCHAR, A.MM) [MM], CONVERT(VARCHAR(4), A.MMM_IN) [Bulan], CONVERT(VARCHAR(4), A.MMM_EN) [Bulan_EN],
			A.Kategori, A.Kategori_EN, ISNULL(B.Jumlah, 0) [Jumlah]
	FROM	(SELECT	*  FROM	RefMonth, @TABLE_Kategori) A
			LEFT JOIN @TABLE B ON A.MM = B.Bulan AND A.Kategori = B.Kategori
	
	
	
END