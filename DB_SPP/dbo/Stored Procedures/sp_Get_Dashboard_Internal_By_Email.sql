CREATE PROCEDURE [dbo].[sp_Get_Dashboard_Internal_By_Email]
	@Email VARCHAR(200) = ''
AS
BEGIN

	--DECLARE @Email VARCHAR(200) = 'spp.ptsmi@gmail.com'

	--SELECT * FROM vw_userInRole WHERE Email = @Email
	--SELECT * FROM tblM_User

	DECLARE @TABLE AS TABLE (Terkirim INT, Diproses INT, Selesai INT, Ditolak INT)

	INSERT INTO @TABLE VALUES (0,0,0,0)

	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Admin SPP')) > 0)
	BEGIN
		UPDATE @TABLE SET Terkirim = (SELECT COUNT(*) [Terkirim] FROM tblT_Dumas WHERE Nomor IS NOT NULL );
		UPDATE @TABLE SET Diproses = (SELECT COUNT(*) [Diproses] FROM tblT_Dumas WHERE Nomor IS NOT NULL  AND [Status] IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut'));
		UPDATE @TABLE SET Selesai = (SELECT COUNT(*) [Selesai] FROM tblT_Dumas WHERE Nomor IS NOT NULL  AND [Status] = 'Selesai') 
		UPDATE @TABLE SET Ditolak = (SELECT COUNT(*) [Ditolak] FROM tblT_Dumas WHERE Nomor IS NOT NULL  AND [Status] = 'Ditolak Admin SPP') 
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
			  
			UPDATE @TABLE SET Terkirim = Terkirim + (SELECT COUNT(*) [Terkirim] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND DelegatorID = @DelegatorID );
			UPDATE @TABLE SET Diproses = Diproses + (SELECT COUNT(*) [Diproses] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] IN ('Diproses'));
			UPDATE @TABLE SET Selesai  = Selesai  + (SELECT COUNT(*) [Selesai]  FROM tblT_Dumas WHERE Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] IN ('Selesai', 'Ditindak lanjut')) 
			UPDATE @TABLE SET Ditolak  = Ditolak  + (SELECT COUNT(*) [Ditolak]  FROM tblT_Dumas WHERE Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] IN ('Ditolak Admin SPP', 'Ditolak Delegator', 'Dihentikan'));

			FETCH NEXT FROM db_cursor INTO @DelegatorID 
		END 

		CLOSE db_cursor  
		DEALLOCATE db_cursor 

		
	END
	

	SELECT * FROM @TABLE
	
END