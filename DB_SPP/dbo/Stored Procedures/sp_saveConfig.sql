-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_saveConfig
	-- Add the parameters for the stored procedure here
	@Request_OTP VARCHAR (150),
	@Submit_OTP VARCHAR (150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   IF(SELECT COUNT(*) FROM TblM_Config) = 0
	BEGIN

	INSERT INTO [TblM_Config]
			   (
			   
				Request_OTP,
				Submit_OTP
				

			   )
		 VALUES
			   (
			    
				@Request_OTP,
				@Submit_OTP
				
			)

	END
	ELSE
	BEGIN

		UPDATE TblM_Config
		SET				
			Request_OTP = @Request_OTP,
			Submit_OTP = @Submit_OTP
			
	END
	
	
END