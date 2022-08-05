









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Overdue_14]
	@Email VARCHAR(200) = ''
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TABLE AS TABLE (ID VARCHAR(36), Nomor VARCHAR(200), Status VARCHAR(100), Tanggal_Kirim VARCHAR(50), Overdue INT)

	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Admin SPP')) > 0)
	BEGIN
		INSERT INTO @TABLE
		SELECT	Convert(varchar(36), A.ID) [ID],
				A.Nomor, A.Status,
				dbo.FormatDate_yyyyMMdd(A.CreatedOn) [Tanggal_Kirim],
				DATEDIFF(DAY, A.CreatedOn, GETDATE()) [Overdue]
		FROM	tblT_Dumas A
		WHERE	A.Status IN ('Terkirim')
				AND DATEDIFF(DAY, A.CreatedOn, GETDATE()) > 14

	END
	ELSE IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Delegator')) > 0)
	BEGIN
		DECLARE @DelegatorID UNIQUEIDENTIFIER;

		DECLARE db_cursor CURSOR FOR 
		SELECT DelegatorID FROM  tblT_UserInDelegator WHERE UserID = (SELECT UserID FROM vw_userInRole WHERE Email = @Email AND Role IN ('Delegator'))

		OPEN db_cursor  
		FETCH NEXT FROM db_cursor INTO @DelegatorID  

		WHILE @@FETCH_STATUS = 0  
		BEGIN  
			
			INSERT INTO @TABLE  
			SELECT	Convert(varchar(36), A.ID) [ID],
					A.Nomor, A.Status,
					dbo.FormatDate_yyyyMMdd(A.CreatedOn) [Tanggal_Kirim],
					DATEDIFF(DAY, A.CreatedOn, GETDATE()) [Overdue]
			FROM	tblT_Dumas A
			WHERE	A.Status IN ('Diproses')
					AND DATEDIFF(DAY, A.CreatedOn, GETDATE()) > 14
					AND A.DelegatorID = @DelegatorID

			FETCH NEXT FROM db_cursor INTO @DelegatorID 
		END 

		CLOSE db_cursor  
		DEALLOCATE db_cursor 

		
	END
	
	SELECT * FROM @TABLE

END