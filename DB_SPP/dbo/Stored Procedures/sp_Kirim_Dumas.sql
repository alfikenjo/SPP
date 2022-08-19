


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Kirim_Dumas]
	@ID uniqueidentifier,
	@Sumber varchar(200),
    @Nomor varchar(200),
    @Email VARCHAR(MAX),
    @Handphone varchar(MAX) = '',
    @TempatKejadian varchar(max),
    @Jenis_Pelanggaran varchar(max),
    @WaktuKejadian datetime,
    @Kronologi varchar(max),
    @Status varchar(50),
    @CreatedBy varchar(MAX)
AS
BEGIN
	
	UPDATE	[tblT_Dumas]
	SET	    [Nomor] = @Nomor			
           ,[Sumber] = ISNULL(NULLIF(@Sumber, ''), 'Portal SPP')
		   ,[Email] = NULLIF(@Email, '')
		   ,[Handphone] = @Handphone		
           ,[TempatKejadian] = @TempatKejadian
           ,[Jenis_Pelanggaran] = @Jenis_Pelanggaran
           ,[WaktuKejadian] = @WaktuKejadian
           ,[Kronologi] = @Kronologi		
           ,[Status] = @Status		
           ,[CreatedBy] = @CreatedBy
		   ,[CreatedOn] = GETDATE()			
	WHERE	ID = @ID

	
END