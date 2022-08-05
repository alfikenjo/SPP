




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Delegator]
	-- Add the parameters for the stored procedure here
	 @Action nvarchar(10),
	 @ID uniqueidentifier,
     @Name nvarchar(255) = '',
     @Description nvarchar(max) = '',
     @isActive int = 1,
	 @CreatedBy nvarchar(200) = ''
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblM_Delegator
			   (
				[ID]
			   ,[Name]
			   ,[Description]
			   ,[isActive]
			   ,[CreatedBy])
		 VALUES
		 (
				@ID,
			    @Name ,
			    NULLIF(@Description, '') ,	
			    @isActive,
				@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Grup Delegator', NULL, 'INSERT', @Name
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE  tblM_Delegator
		SET		Name = @Name,
				Description = NULLIF(@Description, ''),
				isActive = @isActive,
				UpdatedOn = GETDATE(),
				UpdatedBy = @CreatedBy
				
		WHERE	ID = @ID;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Grup Delegator', NULL, 'UPDATE', @Name
	END
	ELSE IF(@Action = 'hapus')
	BEGIN		
		DECLARE @Audit VARCHAR(MAX) = (SELECT Name FROM tblM_Delegator WHERE ID = @ID)
		DELETE FROM tblM_Delegator WHERE ID = @ID;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Grup Delegator', NULL, 'DELETE', @Audit
	END
	
END