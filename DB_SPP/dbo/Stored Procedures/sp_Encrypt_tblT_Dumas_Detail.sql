


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_Dumas_Detail]
	@ID			VARCHAR(36),
	@Nama  VARCHAR(MAX) NULL,
	@NomorHandphone  VARCHAR(MAX) NULL,
	@Departemen  VARCHAR(MAX) NULL,
	@Jabatan  VARCHAR(MAX) NULL,
	@CreatedBy VARCHAR(MAX) NULL

AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_Dumas_Detail]
	SET 
			[Nama] =  NULLIF(@Nama, '')
			,[NomorHandphone] =  NULLIF(@NomorHandphone, '')
			,[Departemen] =  NULLIF(@Departemen, '')
			,[Jabatan] =  NULLIF(@Jabatan, '')
			,[CreatedBy] = NULLIF(@CreatedBy, '')


	 WHERE [ID] = @ID

	

END