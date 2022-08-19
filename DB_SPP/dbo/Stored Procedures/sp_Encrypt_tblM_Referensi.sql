


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblM_Referensi]
	@ID			INT,
	@Updated_By VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;
	 
	UPDATE [tblM_Referensi]
	SET 
		   [Updated_By] = NULLIF(@Updated_By, '')


	 WHERE [ID] = @ID

	

END