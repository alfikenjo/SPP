






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Format_StringNumber]
(	
	-- Add the parameters for the function here
	@String VARCHAR(MAX)
)
RETURNS VARCHAR(50) 
AS BEGIN
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT REPLACE(REPLACE(CONVERT(VARCHAR(MAX), LTRIM(RTRIM(REPLACE(LEFT((REPLACE(REPLACE(REPLACE(@String, '+62', '0'), '-', ''), '0 ', '0')),LEN((REPLACE(REPLACE(REPLACE(@String, '+62', '0'), '-', ''), '0 ', '0')))-CHARINDEX('/',(REPLACE(REPLACE(REPLACE(@String, '+62', '0'), '-', ''), '0 ', '0')))), char(9), '')))), CHAR(13), ''), CHAR(10), '')
)
END