








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateMyProfileInternal]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36),
	@Fullname varchar(MAX) = '',
	@Address varchar(MAX) = '',
	@Gender nvarchar(2) = '',	
	@NIP varchar(MAX) = '',
	@Jabatan varchar(MAX) = '',
	@Divisi varchar(MAX) = '',
	@Img nvarchar(100) = '',
	@Ekstension nvarchar(100) = '',
	@CreatedBy varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE  tblM_User
	SET		
			Fullname = NULLIF(@Fullname, ''),
			Address = NULLIF(@Address, ''),
			Gender = NULLIF(@Gender, ''),
			NIP = NULLIF(@NIP, ''),
			Jabatan = NULLIF(@Jabatan, ''),
			Divisi = NULLIF(@Divisi, ''),
			Img = ISNULL(NULLIF(@Img, ''), Img),
			Ekstension = ISNULL(NULLIF(@Ekstension, ''), Ekstension),
			UpdatedOn = GETDATE(),
			UpdatedBy = NULLIF(@CreatedBy, '')
	WHERE	UserID = @UserID

	DECLARE @Email_Audit VARCHAR(MAX) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
	EXEC sp_RecordAuditTrail @CreatedBy, 'BO My Profile', 'Profile', NULL, 'UPDATE', @CreatedBy
END