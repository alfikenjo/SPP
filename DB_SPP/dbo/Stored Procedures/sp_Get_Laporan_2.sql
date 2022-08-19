
CREATE PROCEDURE [dbo].[sp_Get_Laporan_2]
	@Email VARCHAR(MAX) = '',
	@Start VARCHAR(10),
	@End VARCHAR(10)
AS
BEGIN

	--DECLARE @Email VARCHAR(MAX) = 'spp.ptsmi@gmail.com',
	--		@Start VARCHAR(10) = '2022-01-01',
	--		@End VARCHAR(10) = '2022-07-20'

	DECLARE @TABLE AS TABLE (Delegator VARCHAR(MAX), Masuk INT, Proses INT, Selesai INT)	

	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Admin SPP')) > 0)
	BEGIN
		INSERT INTO @TABLE
		SELECT	A.Name [Delegator],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] NOT IN ('Terkirim')) [Masuk],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] IN ('Diproses')) [Proses],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] NOT IN ('Diproses')) [Selesai]
		FROM	tblM_Delegator A
				
			
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
			SELECT	A.Name [Delegator],
					(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] NOT IN ('Terkirim')) [Masuk],
					(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] IN ('Diproses')) [Proses],
					(SELECT COUNT(*) FROM tblT_Dumas WHERE DelegatorID = A.ID AND Nomor IS NOT NULL AND [Status] NOT IN ('Diproses')) [Selesai]
			FROM	tblM_Delegator A
			WHERE	A.ID = @DelegatorID

			FETCH NEXT FROM db_cursor INTO @DelegatorID 
		END 

		CLOSE db_cursor  
		DEALLOCATE db_cursor 

		
	END
	

	SELECT	Delegator,
			Masuk, Proses, Selesai,
			CAST(ROUND((Selesai * 100.00) / (Masuk * 100.00), 2) * 100.00 AS INT) [Progress]
	FROM	@TABLE
	ORDER BY Masuk DESC
	
END