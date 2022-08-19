



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_NotificationSetting]
	@ID			VARCHAR(36),
	@SMTPAddress           VARCHAR (MAX)    NULL,
    @SMTPPort              VARCHAR (MAX)    NULL,
    @EmailAddress          VARCHAR (MAX)    NULL,
    @Password              VARCHAR (MAX)    NULL,
    @SenderName            VARCHAR (MAX)    NULL,
	@CreatedBy  VARCHAR(MAX) NULL,
	@UpdatedBy  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [NotificationSetting]
	SET 
			SMTPAddress   =  NULLIF(@SMTPAddress, '')
			,SMTPPort     =  NULLIF(@SMTPPort, '')
			,EmailAddress     =  NULLIF(@EmailAddress, '')
			,Password         =  NULLIF(@Password, '')
			,SenderName         =  NULLIF(@SenderName, '')
			,[CreatedBy] =  NULLIF(@CreatedBy, '')
			,[UpdatedBy] =  NULLIF(@UpdatedBy, '')
			

	 WHERE [ID] = @ID

	

END