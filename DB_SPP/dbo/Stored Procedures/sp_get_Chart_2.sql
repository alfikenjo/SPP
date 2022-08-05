




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_Chart_2]
	@StartYear VARCHAR(4),
	@EndYear  VARCHAR(4)
AS
BEGIN

	--DECLARE @StartYear VARCHAR(4) = 2001,
	--@EndYear  VARCHAR(4) = 2022
	
	SELECT CONVERT(VARCHAR(4), Tahun) [Tahun], Kategori, Jumlah + 0 [Jumlah]
	FROM
	(
		SELECT	
				YEAR(A.CreatedOn) [Tahun],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL AND YEAR(CreatedOn) = YEAR(A.CreatedOn) AND Sumber = 'Portal SPP') [Portal],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL AND YEAR(CreatedOn) = YEAR(A.CreatedOn) AND Sumber = 'Email') [Email],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL AND YEAR(CreatedOn) = YEAR(A.CreatedOn) AND Sumber = 'Surat') [Surat],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL AND YEAR(CreatedOn) = YEAR(A.CreatedOn) AND Sumber = 'Telepon') [Telepon],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE Nomor IS NOT NULL AND YEAR(CreatedOn) = YEAR(A.CreatedOn) AND Sumber = 'Fax') [Fax]

		FROM	tblT_Dumas A
		WHERE	YEAR(A.CreatedOn) BETWEEN @StartYear AND @endYear
		GROUP BY YEAR(A.CreatedOn)
	) A
	UNPIVOT
	(
		Jumlah FOR Kategori IN   
       (Portal, Email, Surat, Telepon, Fax)  
	) UNP
	
	
END