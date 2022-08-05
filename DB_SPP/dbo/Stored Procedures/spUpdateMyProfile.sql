







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateMyProfile]
	-- Add the parameters for the stored procedure here
	@UserID varchar(36),
	@Fullname varchar(100) = '',
	@Mobile varchar(50) = '',
	@Address varchar(max) = '',
	@Gender nvarchar(2) = '',	
	@NIP varchar(100) = '',
	@Jabatan varchar(200) = '',
	@Img nvarchar(100) = '',
	@CreatedBy varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE  tblM_User
	SET		
			Fullname = NULLIF(@Fullname, ''),
			Mobile = NULLIF(@Mobile, ''),
			Address = NULLIF(@Address, ''),
			Gender = NULLIF(@Gender, ''),
			NIP = NULLIF(@NIP, ''),
			Jabatan = NULLIF(@Jabatan, ''),
			Img = ISNULL(NULLIF(@Img, ''), Img),
			UpdatedOn = GETDATE(),
			UpdatedBy = NULLIF(@CreatedBy, '')
	WHERE	UserID = @UserID

	EXEC sp_RecordAuditTrail @CreatedBy, 'My Profile', 'Profile', NULL, 'UPDATE', @CreatedBy
END