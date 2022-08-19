


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_Tanggapan]
	@ID			VARCHAR(36),
	@Email  VARCHAR(MAX) NULL,
	@Nama  VARCHAR(MAX) NULL,
	@Tanggapan  VARCHAR(MAX) NULL,
	@CreatedBy  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_Tanggapan]
	SET 
			[Email] =  NULLIF(@Email, '')
			,[Nama] =  NULLIF(@Nama, '')
			,[Tanggapan] =  NULLIF(@Tanggapan, '')
			,[CreatedBy] =  NULLIF(@CreatedBy, '')

	 WHERE [ID] = @ID

	

END