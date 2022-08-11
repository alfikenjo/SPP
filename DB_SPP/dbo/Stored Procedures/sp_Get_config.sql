

CREATE PROCEDURE [dbo].[sp_Get_config]
	
AS
BEGIN

	SELECT	TOP 1 
			
			ISNULL(Request_OTP, '') [Request_OTP],
			ISNULL(Submit_OTP, '') [Submit_OTP]
			

	FROM	TblM_Config
END