
CREATE PROCEDURE [dbo].[sp_Get_Laporan_1]
	@Email VARCHAR(200) = '',
	@Start VARCHAR(10),
	@End VARCHAR(10)
AS
BEGIN

	--DECLARE @Email VARCHAR(200) = 'spp.ptsmi@gmail.com',
	--		@Start VARCHAR(10) = '2022-01-01',
	--		@End VARCHAR(10) = '2022-07-20'

	DECLARE @TABLE AS TABLE (Semua INT, Terkirim INT, Ditolak_Admin_SPP INT, Diproses INT, Ditolak_Delegator INT, Dihentikan INT, Ditindak_lanjut INT, Selesai INT)

	INSERT INTO @TABLE VALUES (0,0,0,0,0,0,0,0)

	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Admin SPP')) > 0)
	BEGIN
		UPDATE @TABLE SET Semua =				(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL);
		UPDATE @TABLE SET Terkirim =			(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Terkirim');
		UPDATE @TABLE SET Ditolak_Admin_SPP =	(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Ditolak Admin SPP');
		UPDATE @TABLE SET Diproses =			(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Diproses');
		UPDATE @TABLE SET Ditolak_Delegator =	(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Ditolak Delegator');
		UPDATE @TABLE SET Dihentikan =			(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Dihentikan');
		UPDATE @TABLE SET Ditindak_lanjut =		(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Ditindak lanjut');
		UPDATE @TABLE SET Selesai =				(SELECT COUNT(*) FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND [Status] = 'Selesai');
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
			  
			UPDATE @TABLE SET Semua = Semua							+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID);
			UPDATE @TABLE SET Terkirim = Terkirim					+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Terkirim');
			UPDATE @TABLE SET Ditolak_Admin_SPP = Ditolak_Admin_SPP + (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Ditolak Admin SPP');
			UPDATE @TABLE SET Diproses = Diproses					+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Diproses');
			UPDATE @TABLE SET Ditolak_Delegator = Ditolak_Delegator + (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Ditolak Delegator');
			UPDATE @TABLE SET Dihentikan = Dihentikan				+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Dihentikan');
			UPDATE @TABLE SET Ditindak_lanjut = Ditindak_lanjut		+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Ditindak lanjut');
			UPDATE @TABLE SET Selesai = Selesai						+ (SELECT COUNT(*)  FROM tblT_Dumas WHERE (dbo.FormatDate_yyyyMMdd(CreatedOn) BETWEEN @Start AND @End) AND Nomor IS NOT NULL AND DelegatorID = @DelegatorID AND [Status] = 'Selesai');

			FETCH NEXT FROM db_cursor INTO @DelegatorID 
		END 

		CLOSE db_cursor  
		DEALLOCATE db_cursor 

		
	END
	

	SELECT * FROM @TABLE
	
END