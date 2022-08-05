








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_User]
	-- Add the parameters for the stored procedure here
	@Action nvarchar(50),
	@UserID varchar(36),
	@PasswordHash varchar(max) = '',
	@Fullname varchar(100) = '',
	@Email varchar(100) = '',
	@Mobile varchar(50) = '',
	@Address varchar(max) = '',
	@Gender nvarchar(2) = '',	
	@NIP varchar(100) = '',
	@Jabatan varchar(200) = '',
	@Divisi varchar(200) = '',
	@ID_Roles varchar(max) = '',
	@Img nvarchar(100) = '',
	@isActive int,
	@CreatedBy varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsPelapor INT = 0;
	DECLARE @IsDelegator INT = 0;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblM_User
			   (
				 UserID
				,PasswordHash
				,Fullname
				,Email
				,Mail_Verification
				,EmailNotification
				,MobileTemp
				,Address
				,Gender				
				,NIP
				,Jabatan
				,Divisi
				,Img
				,isActive
				,CreatedBy
			)
		 VALUES
		 (
				 @UserID
				,@PasswordHash
				,@Fullname
				,@Email
				,1
				,1
				,NULLIF(@Mobile, '')
				,NULLIF(@Address, '')
				,NULLIF(@Gender, '')				
				,NULLIF(@NIP, '')
				,NULLIF(@Jabatan, '')
				,NULLIF(@Divisi, '')
				,NULLIF(@Img, '')
				,@isActive				
				,@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'User Management', 'User', NULL, 'INSERT', @Email
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		SET @IsPelapor = (SELECT COUNT(*) FROM vw_UserInRole WHERE [Role] IN ('Pelapor') AND UserID = @UserID)
		SET @IsDelegator = (SELECT COUNT(*) FROM vw_UserInRole WHERE [Role] IN ('Delegator') AND UserID = @UserID)

		UPDATE  tblM_User
		SET		PasswordHash = ISNULL(NULLIF(@PasswordHash, ''), PasswordHash),
				Fullname = NULLIF(@Fullname, ''),
				Email = NULLIF(@Email, ''),				
				Address = NULLIF(@Address, ''),
				Gender = NULLIF(@Gender, ''),
				NIP = NULLIF(@NIP, ''),
				Jabatan = NULLIF(@Jabatan, ''),
				Divisi = NULLIF(@Divisi, ''),
				Img = ISNULL(NULLIF(@Img, ''), Img),
				isActive = NULLIF(@isActive, ''),
				UpdatedOn = GETDATE(),
				UpdatedBy = NULLIF(@CreatedBy, '')
		WHERE	UserID = @UserID

		IF(SELECT ISNULL(NULLIF(Mobile, ''), '') FROM tblM_User WHERE UserID = @UserID) <> ISNULL(NULLIF(@Mobile, ''), '')
		BEGIN
			UPDATE  tblM_User SET MobileTemp = NULLIF(@Mobile, ''), Mobile = NULL, Mobile_Verification = 0 WHERE UserID = @UserID
		END

		EXEC sp_RecordAuditTrail @CreatedBy, 'User Management', 'User', NULL, 'UPDATE', @Email
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		--DELETE FROM tblT_UserInDelegator WHERE UserID = @UserID
		--DELETE FROM tblT_New_User_Verification WHERE UserID = @UserID
		--DELETE FROM tblT_User_Password_Forgotten WHERE UserID = @UserID
		--DELETE FROM tblM_User WHERE UserID = @UserID;
		UPDATE tblM_User SET isActive = 0, Mobile = NULL, MobileTemp = NULL, Mobile_Verification = 0, IsDeleted = 1, DeletedOn = GETDATE(), DeletedBy = @CreatedBy WHERE UserID = @UserID

		DECLARE @Email_Audit VARCHAR(200) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
		EXEC sp_RecordAuditTrail @CreatedBy, 'User Management', 'User', NULL, 'DELETE', @Email_Audit
	END

	IF(@IsPelapor = 0 AND @IsDelegator = 0) ---->> Usman tidak dapat memodifikasi role atas "Pelapor"
	BEGIN
		DELETE A FROM tblT_UserInRole A JOIN tblM_Role B ON A.RoleID = B.ID WHERE UserID = @UserID;
		IF(@Action <> 'hapus')
		BEGIN
			IF(@ID_Roles <> '')
			BEGIN
				INSERT INTO tblT_UserInRole (UserID, RoleID, CreatedBy)
				SELECT	@UserID, s, @CreatedBy
				FROM	dbo.Split(',', @ID_Roles)				
				--WHERE	s <> (SELECT CONVERT(VARCHAR(36), ID) FROM tblM_Role WHERE Name IN ('Delegator', 'Pelapor'))
				
				DELETE A FROM tblT_UserInRole A JOIN tblM_Role B ON A.RoleID = B.ID WHERE UserID = @UserID AND B.Name IN ('Pelapor', 'Delegator');

			END
		END
	END
	
END