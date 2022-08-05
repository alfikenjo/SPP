





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Kuesioner]
	-- Add the parameters for the stored procedure here
	@Action nvarchar(10),
	@ID				varchar(36),
	@Title			varchar(MAX),
	@StartDate		varchar(10),
	@EndDate		varchar(10),
	@Status			varchar(50),
	@CreatedBy nvarchar(200)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN
		DELETE FROM KuesionerDetailOptions WHERE IDHeader IN (SELECT ID FROM KuesionerDetail WHERE IDHeader = @ID)
		DELETE FROM KuesionerDetail WHERE IDHeader = @ID	
	END


	IF(@Action = 'add')
	BEGIN
		INSERT INTO Kuesioner
		(
			 [ID]
			,[Title]
			,[StartDate]
			,[EndDate]
			,[Status]
			,[CreatedBy]
		)
		 VALUES
		 (
			@ID,
			@Title,
			@StartDate,
			@EndDate,
			@Status,
			@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Manajemen Kuesioner', NULL, 'INSERT', @Title
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		UPDATE  Kuesioner
		SET		
				Title = @Title,
				StartDate = @StartDate,
				EndDate = @EndDate,				
				Status = @Status,
				UpdatedOn = GETDATE(),
				UpdatedBy = @CreatedBy
		WHERE	ID = @ID 

		EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Manajemen Kuesioner', NULL, 'UPDATE', @Title
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		DELETE FROM KuesionerDetailOptions WHERE IDHeader IN (SELECT ID FROM KuesionerDetail WHERE IDHeader = @ID)
		DELETE FROM KuesionerDetail WHERE IDHeader = @ID
		DELETE FROM Kuesioner WHERE ID = @ID;
		EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Manajemen Kuesioner', NULL, 'DELETE', @Title
	END
	
END