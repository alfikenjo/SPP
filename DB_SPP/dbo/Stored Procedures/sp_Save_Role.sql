



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Role]
	-- Add the parameters for the stored procedure here
	 @Action nvarchar(10),
	 @ID varchar(36),
     @Name nvarchar(100) = '',
     @Description nvarchar(max) = '',
     @Status int = 1,
	 @CreatedBy nvarchar(MAX) = ''
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblM_Role
			   (
				[ID]
			   ,[Name]
			   ,[Description]
			   ,[Status]
			   ,[CreatedBy])
		 VALUES
		 (
				@ID,
			    @Name ,
			    NULLIF(@Description, '') ,	
			    @Status,
				@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > User Management', 'Role', NULL, 'INSERT', @Name
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE  tblM_Role
		SET		Name = @Name,
				Description = NULLIF(@Description, ''),
				Status = @Status,
				UpdatedOn = GETDATE(),
				UpdatedBy = @CreatedBy
				
		WHERE	ID = @ID;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > User Management', 'Role', NULL, 'UPDATE', @Name
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		DECLARE @Audit VARCHAR(MAX) = (SELECT Name FROM tblM_Role WHERE ID = @ID)
		DELETE FROM tblM_Role WHERE ID = @ID;
		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > User Management', 'Role', NULL, 'DELETE', @Audit
	END
	
END