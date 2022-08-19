



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Penyaluran]
	@ID varchar(36),
    @Status varchar(200),
    @DelegatorID varchar(36) = '',
    @Jenis_Pelanggaran varchar(max) = '',
    @Keterangan_Penyaluran varchar(max) = '',
    @Keterangan_Penyaluran_Filename varchar(max) = '',
    @Keterangan_Penyaluran_Ekstension varchar(20) = '',     
    @CreatedBy varchar(MAX)
AS
BEGIN
	
	UPDATE	tblT_Dumas
	SET	    Status = @Status           
		   ,DelegatorID = ISNULL(NULLIF(@DelegatorID, ''), DelegatorID)
           ,Jenis_Pelanggaran = @Jenis_Pelanggaran
           ,Keterangan_Penyaluran = @Keterangan_Penyaluran
           ,Keterangan_Penyaluran_Filename = ISNULL(NULLIF(@Keterangan_Penyaluran_Filename, ''), Keterangan_Penyaluran_Filename)
           ,Keterangan_Penyaluran_Ekstension = ISNULL(NULLIF(@Keterangan_Penyaluran_Ekstension, ''), Keterangan_Penyaluran_Ekstension)
           ,PenyaluranBy = @CreatedBy	
           ,PenyaluranDate = GETDATE()				   	
	WHERE	ID = @ID

	
END