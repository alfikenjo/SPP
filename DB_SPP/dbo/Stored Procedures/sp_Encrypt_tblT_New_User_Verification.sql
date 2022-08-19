


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_New_User_Verification]
	@ID			VARCHAR(36),
	@Email VARCHAR(MAX) NULL,
	@CreatedBy VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;
	 
	UPDATE [tblT_New_User_Verification]
	SET 
		   [Email] = NULLIF(@Email, '')
		   ,[CreatedBy] = NULLIF(@CreatedBy, '')


	 WHERE [ID] = @ID

	

END