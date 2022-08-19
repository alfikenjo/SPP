







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateAccount]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36),
	--@Email varchar(255) = '',
	@Mobile varchar(MAX) = '',
	@CreatedBy varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE  tblM_User
	SET					
			MobileTemp = ISNULL(NULLIF(@Mobile, ''), MobileTemp),
			UpdatedOn = GETDATE(),
			UpdatedBy = NULLIF(@CreatedBy, '')
	WHERE	UserID = @UserID

	DECLARE @Email_Audit VARCHAR(MAX) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
	EXEC sp_RecordAuditTrail @CreatedBy, 'My Profile', 'Account', NULL, 'UPDATE', @Email_Audit
END