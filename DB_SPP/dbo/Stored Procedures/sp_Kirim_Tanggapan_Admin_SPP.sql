







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Kirim_Tanggapan_Admin_SPP]
	@ID varchar(36),
	@JenisPengaduan varchar(50),
    @IDPengaduan varchar(36),
    @TipePengirim varchar(50),
    @Email VARCHAR(MAX),
    @Tanggapan varchar(MAX),
	@FileLampiran VARCHAR(200) = NULL,
	@FileLampiran_Ekstension VARCHAR(50) = NULL,
    @CreatedBy varchar(MAX)
AS
BEGIN

	DECLARE @Nama VARCHAR(MAX);
	
	SET @Nama = (SELECT Fullname FROM tblM_User WHERE Email = @Email);

	INSERT INTO [tblT_Tanggapan]
           ([ID],[JenisPengaduan]
		  ,[IDPengaduan]
		  ,[TipePengirim]
		  ,[Email]
		  ,[Nama]
		  ,[Tanggapan]
		  ,[FileLampiran]
		  ,[FileLampiran_Ekstension]		  
		  ,[CreatedBy])
     VALUES
	 (
			@ID,
			@JenisPengaduan,
			@IDPengaduan,
			'Admin SPP',
			@Email,
			ISNULL(NULLIF(@Nama, ''), @Email),
			@Tanggapan,
			NULLIF(@FileLampiran, ''),
			NULLIF(@FileLampiran_Ekstension, ''),
			@CreatedBy
	 )
	
END