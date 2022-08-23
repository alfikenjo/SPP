






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Msg_Tanggapan_by_Email]
    @Email VARCHAR(MAX)	
AS
BEGIN
	--DECLARE @Email VARCHAR(MAX)	= '4+C+CfqQnO8REDtklNoJqGIAVswFLVXoSALwh/BW+qk='
	--SELECT * FROM vw_UserInRole WHERE Email = @Email
	--SELECT ID FROM tblT_Dumas WHERE CreatedBy = @Email AND Nomor IS NOT NULL
	--SELECT * FROM tblT_Tanggapan WHERE IDPengaduan = '9EA43B95-7979-4C89-AAC6-2AA95DD0983E'
	
	DECLARE @TipePengirim VARCHAR(200) = ''

	IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Admin SPP')) > 0)
	BEGIN
		SET @TipePengirim = 'Pelapor'
	END
	ELSE IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Pelapor')) = 1)
	BEGIN
		SET @TipePengirim = 'Admin SPP'
	END

	--SELECT @TipePengirim

	DECLARE @TABLE AS TABLE (Msg_Title VARCHAR(200), Nomor VARCHAR(200), s_CreatedOn VARCHAR(200), Msg_Link VARCHAR(MAX), TipePengirim VARCHAR(200))
	DECLARE @TABLE_RESULT AS TABLE (Msg_Title VARCHAR(200), Nomor VARCHAR(200), s_CreatedOn VARCHAR(200), Msg_Link VARCHAR(MAX), TipePengirim VARCHAR(200))

	IF(@TipePengirim = 'Admin SPP')
	BEGIN		
		INSERT INTO @TABLE 
		SELECT	'Tanggapan Pengaduan' [Msg_Title], 
				(SELECT Nomor FROM tblT_Dumas WHERE ID = A.IDPengaduan) [Nomor],
				dbo.Format24DateTime(A.Createdon) [s_CreatedOn], 
				'/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), A.IDPengaduan) [Msg_Link] ,
				A.TipePengirim
		FROM	tblT_Tanggapan A
		WHERE	A.IDPengaduan IN (SELECT ID FROM tblT_Dumas WHERE Email = @Email AND Nomor IS NOT NULL) 
				AND A.IsRead = 0
				AND A.TipePengirim = @TipePengirim			
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(@TipePengirim = 'Pelapor')
	BEGIN
		INSERT INTO @TABLE 
		SELECT	'Tanggapan Pengaduan' [Msg_Title], 
				(SELECT Nomor FROM tblT_Dumas WHERE ID = A.IDPengaduan) [Nomor],
				dbo.Format24DateTime(A.CreatedOn) [s_CreatedOn], 
				'/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), A.IDPengaduan) [Msg_Link],
				A.TipePengirim
		FROM	tblT_Tanggapan  A
				
		WHERE	A.TipePengirim = @TipePengirim	
				AND A.IsRead = 0
		ORDER BY A.CreatedOn DESC
	END

	INSERT INTO @TABLE_RESULT
	SELECT		TOP 5 A.Msg_Title, A.Nomor, 
				(SELECT TOP 1 dbo.Format24DateTime(CreatedOn) FROM tblT_Tanggapan WHERE IDPengaduan = (SELECT ID FROM tblT_Dumas WHERE Nomor = A.Nomor) ORDER BY CreatedOn DESC) [s_CreatedOn], 
				(SELECT '/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), ID) FROM tblT_Dumas WHERE Nomor = A.Nomor) [Msg_Link],
				A.TipePengirim				
				 
	FROM		@TABLE A
	GROUP BY	A.Msg_Title, A.Nomor, A.TipePengirim
	
	SELECT * FROM @TABLE_RESULT WHERE Nomor IS NOT NULL ORDER BY s_CreatedOn DESC
	
END