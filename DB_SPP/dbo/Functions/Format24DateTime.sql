




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Format24DateTime]
(	
	-- Add the parameters for the function here
	@DATETIME DATETIME
)
RETURNS VARCHAR(50) 
AS BEGIN
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT FORMAT(@DATETIME,'yyyy-MM-dd') + ' '+ CONVERT(VARCHAR(8),CONVERT(DATETIME, @DATETIME, 0), 14)
)
END