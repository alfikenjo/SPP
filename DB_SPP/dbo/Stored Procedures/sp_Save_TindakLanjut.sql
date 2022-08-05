




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_TindakLanjut]
	@ID varchar(36),
    @Status varchar(200),
    @Keterangan_Pemeriksaan varchar(max) = '',
    @Keterangan_Pemeriksaan_Filename varchar(max) = '',
    @Keterangan_Pemeriksaan_Ekstension varchar(20) = '',     
	@Keterangan_Konfirmasi varchar(max) = '',
    @Keterangan_Konfirmasi_Filename varchar(max) = '',
    @Keterangan_Konfirmasi_Ekstension varchar(20) = '',     
    @CreatedBy varchar(200)
AS
BEGIN
	
	UPDATE	tblT_Dumas
	SET	    Status = @Status           
           ,Keterangan_Pemeriksaan = @Keterangan_Pemeriksaan
           ,Keterangan_Pemeriksaan_Filename = ISNULL(NULLIF(@Keterangan_Pemeriksaan_Filename, ''), Keterangan_Pemeriksaan_Filename)
           ,Keterangan_Pemeriksaan_Ekstension = ISNULL(NULLIF(@Keterangan_Pemeriksaan_Ekstension, ''), Keterangan_Pemeriksaan_Ekstension)
		   ,Keterangan_Konfirmasi = @Keterangan_Konfirmasi
           ,Keterangan_Konfirmasi_Filename = ISNULL(NULLIF(@Keterangan_Konfirmasi_Filename, ''), Keterangan_Konfirmasi_Filename)
           ,Keterangan_Konfirmasi_Ekstension = ISNULL(NULLIF(@Keterangan_Konfirmasi_Ekstension, ''), Keterangan_Konfirmasi_Ekstension)
           ,TindakLanjutBy = @CreatedBy	
           ,TindakLanjutDate = GETDATE()				   	
	WHERE	ID = @ID

	
END