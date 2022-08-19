


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_Dumas_Detail_2]
	@ID			VARCHAR(36),
	@CreatedBy VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;
	 
	UPDATE [tblT_Dumas_Detail]
	SET 
		   [CreatedBy] = NULLIF(@CreatedBy, '')


	 WHERE [ID] = @ID

	

END