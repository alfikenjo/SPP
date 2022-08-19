









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Cek_Pengaduan_By_Delegator]
	@Email VARCHAR(MAX),
	@ID VARCHAR(36) 
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TABLE AS TABLE (ID VARCHAR(36));
	DECLARE @DelegatorID UNIQUEIDENTIFIER;

	DECLARE db_cursor CURSOR FOR 
	SELECT DelegatorID FROM  tblT_UserInDelegator WHERE UserID = (SELECT UserID FROM vw_userInRole WHERE Email = @Email AND Role IN ('Delegator'))

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @DelegatorID  

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
			
		INSERT INTO @TABLE  
		SELECT	CONVERT(VARCHAR(36), A.ID)
		FROM	tblT_Dumas A
		WHERE	A.DelegatorID = @DelegatorID

		FETCH NEXT FROM db_cursor INTO @DelegatorID 
	END 

	CLOSE db_cursor  
	DEALLOCATE db_cursor 

	SELECT COUNT(*) [Count] FROM @TABLE WHERE ID = @ID

	

END