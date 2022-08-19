

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblM_User]
	@UserID			VARCHAR(36),
	@Fullname		VARCHAR(MAX) NULL,
	@Email			VARCHAR(MAX) NULL,
	@Mobile			VARCHAR(MAX) NULL,
	@MobileTemp		VARCHAR(MAX) NULL,
	@Address		VARCHAR(MAX) NULL,
	@NIP			VARCHAR(MAX) NULL,
	@Jabatan		VARCHAR(MAX) NULL,
	@Divisi			VARCHAR(MAX) NULL,
	@CreatedBy		VARCHAR(MAX) NULL,
	@UpdatedBy		VARCHAR(MAX) NULL,
	@DeletedBy		VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblM_User]
	SET 
		   [Fullname] =		NULLIF(@Fullname, '')
		  ,[Email] =		NULLIF(@Email, '')
		  ,[Mobile] =		NULLIF(@Mobile, '')
		  ,[MobileTemp] =	NULLIF(@MobileTemp, '')
		  ,[Address] =		NULLIF(@Address, '')
		  ,[NIP] =			NULLIF(@NIP, '')
		  ,[Jabatan] =		NULLIF(@Jabatan, '')
		  ,[Divisi] =		NULLIF(@Divisi, '')
		  ,[CreatedBy] =	NULLIF(@CreatedBy, '')
		  ,[UpdatedBy] =	NULLIF(@UpdatedBy, '')
		  ,[DeletedBy] =	NULLIF(@DeletedBy, '')
	 WHERE [UserID] =		@UserID

	

END