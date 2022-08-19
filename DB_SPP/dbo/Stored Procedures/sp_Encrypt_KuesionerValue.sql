


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_KuesionerValue]
	@ID			VARCHAR(36),
	@CreatedBy VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;
	 
	UPDATE [KuesionerValue]
	SET 
		   [CreatedBy] = NULLIF(@CreatedBy, '')


	 WHERE [ID] = @ID

	

END