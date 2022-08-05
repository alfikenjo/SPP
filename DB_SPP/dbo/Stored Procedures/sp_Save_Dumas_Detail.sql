






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Dumas_Detail]
	@Action varchar(50),
	@ID varchar(36),
	@ID_Header varchar(36),

	@Email varchar(200) = '',
	@Handphone varchar(200) = '',

    @Nama varchar(200) = '',
    @NomorHandphone varchar(200) = '',
    @Departemen varchar(200) = '',
    @Jabatan varchar(200) = '',
    @FileIdentitas varchar(255) = '',
    @FileIdentitas_Ekstension varchar(50) = '',
    @CreatedBy varchar(200)
AS
BEGIN	

	
	
	IF(@Action = 'add')
	BEGIN
		IF(NULLIF(@ID_Header, '') IS NULL)
		BEGIN
			SET @ID_Header = NEWID();
			INSERT INTO tblT_Dumas (ID, Email, Handphone, CreatedBy)
			VALUES (
				@ID_Header,
				NULLIF(@Email, ''),
				@Handphone,
				@CreatedBy
			)
		END
	

		INSERT INTO tblT_Dumas_Detail
				   ([ID]
				   ,[ID_Header]
				   ,[Nama]
				   ,[NomorHandphone]
				   ,[Departemen]
				   ,[Jabatan]
				   ,[FileIdentitas]
				   ,[FileIdentitas_Ekstension]
				   ,[CreatedBy])
			 VALUES
			 (
					@ID, 
					@ID_Header, 
					@Nama, 
					@NomorHandphone,
					@Departemen, 
					@Jabatan, 
					@FileIdentitas, 
					@FileIdentitas_Ekstension, 
					@CreatedBy
			 )
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE	tblT_Dumas_Detail
		SET		Nama = @Nama, 
				NomorHandphone = ISNULL(NULLIF(@NomorHandphone, ''), NomorHandphone),
				Departemen = ISNULL(NULLIF(@Departemen, ''), Departemen), 
				Jabatan = ISNULL(NULLIF(@Jabatan, ''), Jabatan), 
				FileIdentitas = ISNULL(NULLIF(@FileIdentitas, ''), FileIdentitas), 
				FileIdentitas_Ekstension = ISNULL(NULLIF(@FileIdentitas_Ekstension, ''), FileIdentitas_Ekstension), 
				UpdatedBy = @CreatedBy,
				UpdatedOn = GETDATE()
		WHERE	ID = @ID
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		DELETE FROM	tblT_Dumas_Detail WHERE	ID = @ID
	END
	
	SELECT @ID_Header [ID_Header]
	
END