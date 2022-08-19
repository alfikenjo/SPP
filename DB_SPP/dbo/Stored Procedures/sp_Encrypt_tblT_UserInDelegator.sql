



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_UserInDelegator]
	@ID			VARCHAR(36),
	@CreatedBy  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_UserInDelegator]
	SET 
			[CreatedBy] =  NULLIF(@CreatedBy, '')
			
			

	 WHERE [ID] = @ID

	

END