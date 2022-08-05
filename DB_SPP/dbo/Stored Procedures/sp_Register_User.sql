


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Register_User]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36),
	@New_User_Verification_ID varchar(36),
	@Fullname varchar(200) = '',
	@Email varchar(100) = '',
	@Mobile varchar(50) = '',
	@PasswordHash varchar(max) = '',
	@isActive int,
	@CreatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO tblM_User
			   (
				 UserID
				,Fullname
				,Email
				,MobileTemp
				,PasswordHash
				,isActive
				,CreatedBy
			)
		 VALUES
		 (
				 @UserID
				,NULLIF(@Fullname, '')
				,@Email
				,NULLIF(@Mobile, '')
				,@PasswordHash
				,@isActive				
				,@CreatedBy
		 )

	IF(SELECT COUNT(*) FROM tblM_User WHERE UserID <> @UserID AND Email = @Email AND ISNULL(isActive, 0) = 0) > 0
	BEGIN
		DELETE FROM tblT_New_User_Verification WHERE UserID IN (SELECT UserID FROM tblM_User WHERE UserID <> @UserID AND Email = @Email AND ISNULL(isActive, 0) = 0)
		DELETE FROM tblM_User WHERE UserID <> @UserID AND Email = @Email AND ISNULL(isActive, 0) = 0
	END

	EXEC sp_RecordAuditTrail @CreatedBy, 'SPP Website', 'Register', NULL, 'INSERT', @Email
	
	UPDATE tblT_New_User_Verification SET Status = 'Not Valid' WHERE Email = @Email AND UserID <> @UserID 
	--DELETE FROM tblM_User WHERE Email = @Email AND UserID <> @UserID AND isActive = 0

	INSERT INTO tblT_New_User_Verification (ID, UserID, Email, Status, CreatedBy)
	SELECT @New_User_Verification_ID, @UserID, @Email, 'Not verified', @Email
	
END