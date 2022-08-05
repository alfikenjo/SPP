

CREATE PROCEDURE [dbo].[sp_Get_NotificationSetting]
	
AS
BEGIN

	SELECT	TOP 1 
			CONVERT(VARCHAR(36), ID) [ID],
			ISNULL(SMTPAddress, '') [SMTPAddress],
			ISNULL(SMTPPort, '') [SMTPPort],
			ISNULL(EmailAddress, '') [EmailAddress],
			ISNULL(Password, '') [Password],
			ISNULL(SenderName, '') [SenderName],
			EnableSSL,
			UseDefaultCredentials,
			UseAsync,
			EnableServices,			

			NewUser,
			NewRoleAssignment,
			UserPasswordReset,
			Messaging,
			ReminderServices,

			'Updated by: ' + ISNULL(UpdatedBy, CreatedBy) + ' @' + dbo.Format24DateTime(ISNULL(UpdatedOn, CreatedOn))  [UpdatedOn]

	FROM	NotificationSetting
END