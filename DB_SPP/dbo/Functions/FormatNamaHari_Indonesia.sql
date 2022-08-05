






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FormatNamaHari_Indonesia]
(	
	-- Add the parameters for the function here
	@DATE DATE
)


RETURNS VARCHAR(50) 
AS BEGIN

RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT 
	CASE 
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Sunday' THEN 'Minggu'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Monday' THEN 'Senin'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Tuesday' THEN 'Selasa'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Wednesday' THEN 'Rabu'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Thursday' THEN 'Kamis'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Friday' THEN 'Jumat'
		WHEN (DATENAME(dw, CAST(DATEPART(m, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(d, @DATE) AS VARCHAR) + '/' + CAST(DATEPART(yy, @DATE) AS VARCHAR))) = 'Saturday' THEN 'Sabtu'
	END
)
END