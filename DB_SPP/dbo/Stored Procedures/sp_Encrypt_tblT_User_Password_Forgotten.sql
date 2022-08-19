




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_User_Password_Forgotten]
	@ID			VARCHAR(36),
	@Email VARCHAR(MAX) NULL,
	@CreatedBy  VARCHAR(MAX) NULL,
	@UpdatedBy  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_User_Password_Forgotten]
	SET 
			[Email] =  NULLIF(@Email, '')
			,[CreatedBy] =  NULLIF(@CreatedBy, '')
			,[UpdatedBy] =  NULLIF(@UpdatedBy, '')
			

	 WHERE [ID] = @ID

	

END