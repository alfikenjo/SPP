





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Format24DateTime_Control]
(	
	-- Add the parameters for the function here
	@DATETIME DATETIME
)
RETURNS VARCHAR(50) 
AS BEGIN
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT FORMAT(@DATETIME,'yyyy-MM-ddT') + CONVERT(VARCHAR(5),CONVERT(DATETIME, @DATETIME, 0), 108)
)
END