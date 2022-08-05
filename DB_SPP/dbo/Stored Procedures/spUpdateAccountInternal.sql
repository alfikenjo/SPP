








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateAccountInternal]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36),	
	@Mobile varchar(100) = '',
	@EmailNotification int,
	@CreatedBy varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE  tblM_User
	SET					
			MobileTemp = ISNULL(NULLIF(@Mobile, ''), MobileTemp),
			EmailNotification = @EmailNotification,
			UpdatedOn = GETDATE(),
			UpdatedBy = NULLIF(@CreatedBy, '')
	WHERE	UserID = @UserID

	DECLARE @Email_Audit VARCHAR(200) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
	EXEC sp_RecordAuditTrail @CreatedBy, 'BO SPP - My Profile', 'Account', NULL, 'UPDATE', @Email_Audit
END