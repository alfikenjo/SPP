










-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GrantedFile]
	@Filename VARCHAR(MAX),
	@Email VARCHAR(200)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TABLE AS TABLE (Filename VARCHAR(MAX))
	INSERT INTO @TABLE
	SELECT Img FROM tblM_User WHERE Img = @Filename AND Email = @Email

	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('Admin SPP')) > 0)
	BEGIN
		
		INSERT INTO @TABLE  
		SELECT A.FileEvidence FROM tblT_File_Evidence A JOIN tblT_Dumas B ON A.ID_Header = B.ID WHERE A.FileEvidence = @Filename

		INSERT INTO @TABLE  
		SELECT A.FileIdentitas FROM tblT_Dumas_Detail A JOIN tblT_Dumas B ON A.ID_Header = B.ID WHERE A.FileIdentitas = @Filename

		INSERT INTO @TABLE  
		SELECT A.FileLampiran FROM tblT_Tanggapan A JOIN tblT_Dumas B ON A.IDPengaduan = B.ID WHERE A.FileLampiran = @Filename

		INSERT INTO @TABLE  
		SELECT Keterangan_Penyaluran_Filename FROM tblT_Dumas WHERE Keterangan_Penyaluran_Filename = @Filename

		INSERT INTO @TABLE  
		SELECT Keterangan_Konfirmasi_Filename FROM tblT_Dumas WHERE Keterangan_Konfirmasi_Filename = @Filename

		INSERT INTO @TABLE  
		SELECT Keterangan_Pemeriksaan_Filename FROM tblT_Dumas WHERE Keterangan_Pemeriksaan_Filename = @Filename 

		INSERT INTO @TABLE  
		SELECT Keterangan_Respon_Filename FROM tblT_Dumas WHERE Keterangan_Respon_Filename = @Filename 

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
			SELECT A.FileEvidence FROM tblT_File_Evidence A JOIN tblT_Dumas B ON A.ID_Header = B.ID WHERE A.FileEvidence = @Filename AND B.DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT A.FileIdentitas FROM tblT_Dumas_Detail A JOIN tblT_Dumas B ON A.ID_Header = B.ID WHERE A.FileIdentitas = @Filename AND B.DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT A.FileLampiran FROM tblT_Tanggapan A JOIN tblT_Dumas B ON A.IDPengaduan = B.ID WHERE A.FileLampiran = @Filename AND B.DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT Keterangan_Penyaluran_Filename FROM tblT_Dumas WHERE Keterangan_Penyaluran_Filename = @Filename AND DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT Keterangan_Konfirmasi_Filename FROM tblT_Dumas WHERE Keterangan_Konfirmasi_Filename = @Filename AND DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT Keterangan_Pemeriksaan_Filename FROM tblT_Dumas WHERE Keterangan_Pemeriksaan_Filename = @Filename AND DelegatorID = @DelegatorID

			INSERT INTO @TABLE  
			SELECT Keterangan_Respon_Filename FROM tblT_Dumas WHERE Keterangan_Respon_Filename = @Filename AND DelegatorID = @DelegatorID

			FETCH NEXT FROM db_cursor INTO @DelegatorID 
		END 

		CLOSE db_cursor  
		DEALLOCATE db_cursor 

	END
	IF((SELECT COUNT(*) FROM vw_userInRole WHERE Email = @Email AND Role IN ('System Administrator')) > 0)
	BEGIN
		
		INSERT INTO @TABLE  
		SELECT Filename FROM tblT_Banner WHERE Filename = @Filename

		INSERT INTO @TABLE  
		SELECT Filename FROM tblT_CMS WHERE Filename = @Filename

		INSERT INTO @TABLE  
		SELECT Filename1 FROM tblT_CMS WHERE Filename1 = @Filename

	END
	
	IF(SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email) = 0
	BEGIN
		DELETE FROM @TABLE
	END

	SELECT COUNT(*) [Count] FROM @TABLE

END