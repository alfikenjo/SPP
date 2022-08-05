










-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateEmail]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36) = '',
	@Email varchar(200) = '',
	@EmailNotification int,	
	@CreatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE	tblM_User 
	SET		Email = ISNULL(NULLIF(@Email, ''), Email), 
			EmailNotification = @EmailNotification,
			UpdatedBy = @CreatedBy, UpdatedOn = GETDATE()
	WHERE	UserID = @UserID

	DECLARE @Email_Audit VARCHAR(200) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
	EXEC sp_RecordAuditTrail @CreatedBy, 'My Profile', 'Email', NULL, 'UPDATE', @Email_Audit
	
END