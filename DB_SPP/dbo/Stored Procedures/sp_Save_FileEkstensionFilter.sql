




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_FileEkstensionFilter]
	-- Add the parameters for the stored procedure here
	 @Action nvarchar(10),
	 @ID varchar(36),
     @Name nvarchar(50) = '',
	 @CreatedBy nvarchar(MAX) = ''
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO FileEkstensionFilter
			   (
				[ID]
			   ,[Name]
			   ,[CreatedBy])
		 VALUES
		 (
				@ID,
			    @Name ,
				@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Ekstension Filter', NULL, 'INSERT', @Name
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE  FileEkstensionFilter
		SET		Name = @Name,
				UpdatedOn = GETDATE(),
				UpdatedBy = @CreatedBy
				
		WHERE	ID = @ID;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Ekstension Filter', NULL, 'UPDATE', @Name
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		DECLARE @Audit VARCHAR(MAX) = (SELECT Name FROM FileEkstensionFilter WHERE ID = @ID)
		DELETE FROM FileEkstensionFilter WHERE ID = @ID;
		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Ekstension Filter', NULL, 'DELETE', @Audit
	END
	
END