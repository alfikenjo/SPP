




-- =============================================
-- Author:		<Author,,Type>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_tblM_Referensi]
	-- Add the parameters for the stored procedure here
	 @Action nvarchar(10),
	 @ID INT,
     @Type nvarchar(MAX),
	 @Value nvarchar(MAX),
	 @Description nvarchar(MAX),
	 @CreatedBy nvarchar(200)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblM_Referensi
			   (
				[ID]
			   ,[Type]
			   ,[Value]
			   ,[Description]
			   ,[Created_By])
		 VALUES
		 (
				@ID,
			    @Type,
				@Value,
				@Description,
				@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Upload', NULL, 'INSERT', @Type
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE  tblM_Referensi
		SET		Value = @Value,
				Updated_On = GETDATE(),
				Updated_By = @CreatedBy
				
		WHERE	Type = @Type;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Upload', NULL, 'UPDATE', @Type
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		DECLARE @Audit VARCHAR(MAX) = (SELECT Type FROM tblM_Referensi WHERE ID = @ID)
		DELETE FROM tblM_Referensi WHERE ID = @ID;
		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > Setting', 'File Upload', NULL, 'DELETE', @Audit
	END
	
END