







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FormatDate_yyyyMM]
(	
	-- Add the parameters for the function here
	@DATETIME VARCHAR(30)
)


RETURNS VARCHAR(6) 
AS BEGIN

DECLARE @VDATE DATE;

IF(ISDATE(@DATETIME) = 1)
BEGIN
	SET @VDATE = @DATETIME;
END	

ELSE
BEGIN
	SET @VDATE = NULL
END

DECLARE @VALUE VARCHAR(50);
IF(YEAR(@VDATE) > 2030)
BEGIN
	SET @VALUE = NULL;
END

ELSE IF(@VDATE IS NOT NULL)
BEGIN
	SET @VALUE = (SELECT FORMAT(@VDATE,'yyyyMM'))
END

ELSE 
BEGIN
	SET @VALUE = NULL;
END

IF(@VALUE = '1900-01-01')
BEGIN
	SET @VALUE = NULL
END

RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT @Value
)
END


--SELECT dbo.FormatDate_yyyyMMdd(NULL);