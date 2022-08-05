





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_EmailSetting]
	-- Add the parameters for the stored procedure here
	@Action				VARCHAR(10),
	@ID					VARCHAR(36),
	@Tipe    VARCHAR(MAX),
	--@Lang    VARCHAR(MAX),
	@Subject    VARCHAR(MAX),
	@Konten    VARCHAR(MAX),
	--@DelegatorName    VARCHAR(MAX),
	--@Email    VARCHAR(MAX),
	--@EmailPelapor    VARCHAR(MAX),
	--@Fullname    VARCHAR(MAX),
	--@IDValue    VARCHAR(MAX),
	--@New_User_Verification_ID    VARCHAR(MAX),
	--@NewRandomPassword    VARCHAR(MAX),
	--@Nomor    VARCHAR(MAX),
	--@Roles    VARCHAR(MAX),
	@Status    VARCHAR(MAX),
	--@TanggalKirim    VARCHAR(MAX),
	--@New_User_Password_Forgotten_ID    VARCHAR(MAX),
	@CreatedBy    VARCHAR(MAX)
		 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Entity VARCHAR(200) = 'Sys Administrator > Setting > Email Template > '+ @Tipe;

	IF(@Action = 'edit')
	BEGIN
		UPDATE  tblT_EmailSetting
		SET		
				Subject= @Subject, 
				Konten= @Konten, 
				Status= @Status, 
				UpdatedOn = GETDATE(),
				UpdatedBy = @CreatedBy
		WHERE	ID = @ID;
		EXEC sp_RecordAuditTrail @CreatedBy, @Entity, @Tipe, NULL, 'UPDATE', @ID
	END
END