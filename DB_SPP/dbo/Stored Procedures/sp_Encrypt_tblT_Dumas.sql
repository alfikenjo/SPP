


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_Dumas]
	@ID			VARCHAR(36),
	@Email  VARCHAR(MAX) NULL,
	@Handphone  VARCHAR(MAX) NULL,
	@TempatKejadian  VARCHAR(MAX) NULL,
	@Kronologi  VARCHAR(MAX) NULL,
	@PenyaluranBy  VARCHAR(MAX) NULL,
	@TindakLanjutBy  VARCHAR(MAX) NULL,
	@ResponBy  VARCHAR(MAX) NULL,
	@CreatedBy  VARCHAR(MAX) NULL,
	@UpdatedBy  VARCHAR(MAX) NULL,
	@ProsesBy  VARCHAR(MAX) NULL,
	@Jenis_Pelanggaran  VARCHAR(MAX) NULL,
	@Keterangan_Penyaluran  VARCHAR(MAX) NULL,
	@Keterangan_Pemeriksaan  VARCHAR(MAX) NULL,
	@Keterangan_Konfirmasi  VARCHAR(MAX) NULL,
	@Keterangan_Respon  VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_Dumas]
	SET 
			[Email] =  NULLIF(@Email, '')
			,[Handphone] =  NULLIF(@Handphone, '')
			,[TempatKejadian] =  NULLIF(@TempatKejadian, '')
			,[Kronologi] =  NULLIF(@Kronologi, '')
			,[PenyaluranBy] =  NULLIF(@PenyaluranBy, '')
			,[TindakLanjutBy] =  NULLIF(@TindakLanjutBy, '')
			,[ResponBy] =  NULLIF(@ResponBy, '')
			,[CreatedBy] =  NULLIF(@CreatedBy, '')
			,[UpdatedBy] =  NULLIF(@UpdatedBy, '')
			,[ProsesBy] =  NULLIF(@ProsesBy, '')
			,[Jenis_Pelanggaran] =  NULLIF(@Jenis_Pelanggaran, '')
			,[Keterangan_Penyaluran] =  NULLIF(@Keterangan_Penyaluran, '')
			,[Keterangan_Pemeriksaan] =  NULLIF(@Keterangan_Pemeriksaan, '')
			,[Keterangan_Konfirmasi] =  NULLIF(@Keterangan_Konfirmasi, '')
			,[Keterangan_Respon] =  NULLIF(@Keterangan_Respon, '')

	 WHERE [ID] = @ID

	

END