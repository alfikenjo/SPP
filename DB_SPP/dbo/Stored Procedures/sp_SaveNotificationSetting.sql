




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveNotificationSetting]
	-- Add the parameters for the stored procedure here	 
    @SMTPAddress VARCHAR (200),
	@SMTPPort VARCHAR (200),
	@EmailAddress VARCHAR (200),
	@Password VARCHAR (200),
	@SenderName VARCHAR (200),
	@EnableSSL BIT,
	@UseDefaultCredentials BIT,
	@UseAsync BIT,
	@EnableServices BIT,
	@CreatedBy VARCHAR (200),

	@NewUser BIT,
	@NewRoleAssignment BIT,
	@UserPasswordReset BIT,
	@Messaging BIT,
	@ReminderServices BIT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(SELECT COUNT(*) FROM NotificationSetting) = 0
	BEGIN

	INSERT INTO [NotificationSetting]
			   (
				ID,
				SMTPAddress,
				SMTPPort,
				EmailAddress,
				Password,
				SenderName,
				EnableSSL,
				UseDefaultCredentials,
				UseAsync,
				EnableServices,
				CreatedBy,

				NewUser,
				NewRoleAssignment,
				UserPasswordReset,
				Messaging,
				ReminderServices

			   )
		 VALUES
			   (
			    NEWID(),
				@SMTPAddress,
				@SMTPPort,
				@EmailAddress,
				@Password,
				@SenderName,
				@EnableSSL,
				@UseDefaultCredentials,
				@UseAsync,
				@EnableServices,
				@CreatedBy,

				@NewUser,
				@NewRoleAssignment,
				@UserPasswordReset,
				@Messaging,
				@ReminderServices
			)

	END
	ELSE
	BEGIN

		UPDATE NotificationSetting
		SET				
			SMTPAddress = @SMTPAddress,
			SMTPPort = @SMTPPort,
			EmailAddress = @EmailAddress,
			Password = @Password,
			SenderName = @SenderName,
			EnableSSL = @EnableSSL,
			UseDefaultCredentials = @UseDefaultCredentials,
			UseAsync = @UseAsync,
			EnableServices = @EnableServices,

			NewUser = @NewUser,
			NewRoleAssignment = @NewRoleAssignment,
			UserPasswordReset = @UserPasswordReset,
			Messaging = @Messaging,
			ReminderServices = @ReminderServices,
			UpdatedBy = @CreatedBy,
			[UpdatedOn] = GETDATE()
	END
	
	EXEC sp_RecordAuditTrail @CreatedBy, 'Administrator', 'Setting', 'Notification Setting', 'Update', 'Success'
END