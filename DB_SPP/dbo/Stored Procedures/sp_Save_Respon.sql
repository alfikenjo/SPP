




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Respon]
	@ID varchar(36),
    @Status varchar(200),
    @Keterangan_Respon varchar(max) = '',
    @Keterangan_Respon_Filename varchar(max) = '',
    @Keterangan_Respon_Ekstension varchar(20) = '',     
    @CreatedBy varchar(200)
AS
BEGIN
	
	UPDATE	tblT_Dumas
	SET	    Status = @Status           
           ,Keterangan_Respon = @Keterangan_Respon
           ,Keterangan_Respon_Filename = ISNULL(NULLIF(@Keterangan_Respon_Filename, ''), Keterangan_Respon_Filename)
           ,Keterangan_Respon_Ekstension = ISNULL(NULLIF(@Keterangan_Respon_Ekstension, ''), Keterangan_Respon_Ekstension)
           ,ResponBy = @CreatedBy	
           ,ResponDate = GETDATE()				   	
	WHERE	ID = @ID

	
END