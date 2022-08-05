





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FormatNamaBulan_Indonesia]
(	
	-- Add the parameters for the function here
	@DATE VARCHAR(30)
)


RETURNS VARCHAR(50) 
AS BEGIN

DECLARE @VDATE DATE;

IF(ISDATE(@DATE) = 1)
BEGIN
	SET @VDATE = @DATE;
END	

ELSE
BEGIN
	SET @VDATE = NULL
END

DECLARE @VALUE VARCHAR(50);
IF(YEAR(@VDATE) > 2030 OR YEAR(@VDATE) < 1900)
BEGIN
	SET @VALUE = NULL;
END

ELSE IF(@VDATE IS NOT NULL)
BEGIN
	SET @VALUE = (SELECT FORMAT(@VDATE,'MMMM'))
	SET @VALUE = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@VALUE, 'January', 'Januari'), 'February', 'Februari'), 'March', 'Maret'), 'May', 'Mei'), 'June', 'Juni'), 'July', 'Juli'), 'August', 'Agustus'), 'October', 'Oktober'), 'December', 'Desember')
		
END
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT @Value
)
END