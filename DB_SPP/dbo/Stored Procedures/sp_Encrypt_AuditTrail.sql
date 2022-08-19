



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_AuditTrail]
	@ID			VARCHAR(36),
	@Username  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [AuditTrail]
	SET 
			[Username] =  NULLIF(@Username, '')
			
	 WHERE [ID] = @ID

	

END