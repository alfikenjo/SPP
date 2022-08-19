







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Msg_Tanggapan_Internal_by_Email]
    @Email VARCHAR(MAX)	
AS
BEGIN
	--DECLARE @Email VARCHAR(MAX)	= 'alfi.kenjo@gmail.com' --'mh.alfi.syahri@gmail.com' --
	
	DECLARE @TipePengirim VARCHAR(200) = ''
	DECLARE @UserID UNIQUEIDENTIFIER = (SELECT TOP 1 UserID FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Delegator', 'Admin SPP'))

	IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Admin SPP')) > 0)
	BEGIN
		SET @TipePengirim = 'Internal - Delegator'
	END
	ELSE IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Delegator')) > 0)
	BEGIN
		SET @TipePengirim = 'Internal - Admin SPP'
	END

	--SELECT @TipePengirim

	DECLARE @TABLE AS TABLE (Msg_Title VARCHAR(200), Nomor VARCHAR(200), s_CreatedOn VARCHAR(200), Msg_Link VARCHAR(MAX), TipePengirim VARCHAR(200), DelegatorName VARCHAR(200))
	DECLARE @TABLE_RESULT AS TABLE (Msg_Title VARCHAR(200), Nomor VARCHAR(200), s_CreatedOn VARCHAR(200), Msg_Link VARCHAR(MAX), TipePengirim VARCHAR(200), DelegatorName VARCHAR(200))

	IF(@TipePengirim = 'Internal - Admin SPP')
	BEGIN
		INSERT INTO @TABLE 
		SELECT	'Tanggapan Pengaduan' [Msg_Title], 
				B.Nomor,
				dbo.Format24DateTime(A.Createdon) [s_CreatedOn], 
				'/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), A.IDPengaduan) [Msg_Link],
				A.TipePengirim,
				C.Name [DelegatorName]
		FROM	tblT_Tanggapan  A
				JOIN tblT_Dumas B ON A.IDPengaduan = B.ID --AND B.DelegatorID IN (SELECT DelegatorID FROM tblT_UserInDelegator WHERE UserID = @UserID)				
				JOIN tblM_Delegator C ON B.DelegatorID = C.ID
				JOIN tblT_UserInDelegator D ON C.ID = D.DelegatorID AND D.UserID = @UserID
		WHERE	A.IsRead = 0
				AND A.TipePengirim = @TipePengirim			
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(@TipePengirim = 'Internal - Delegator')
	BEGIN
		INSERT INTO @TABLE 
		SELECT	'Tanggapan Pengaduan' [Msg_Title], 
				(SELECT Nomor FROM tblT_Dumas WHERE ID = A.IDPengaduan) [Nomor],
				dbo.Format24DateTime(A.CreatedOn) [s_CreatedOn], 
				'/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), A.IDPengaduan) [Msg_Link],
				A.TipePengirim,
				ISNULL(C.Name, '') [DelegatorName]
		FROM	tblT_Tanggapan  A
				JOIN tblT_Dumas B ON A.IDPengaduan = B.ID
				LEFT JOIN tblM_Delegator C ON B.DelegatorID = C.ID
		WHERE	A.TipePengirim = @TipePengirim	
				AND A.IsRead = 0
		ORDER BY A.CreatedOn DESC
	END

	INSERT INTO @TABLE_RESULT
	SELECT		TOP 5 A.Msg_Title, A.Nomor, 
				(SELECT TOP 1 dbo.Format24DateTime(CreatedOn) FROM tblT_Tanggapan WHERE IDPengaduan = (SELECT ID FROM tblT_Dumas WHERE Nomor = A.Nomor) ORDER BY CreatedOn DESC) [s_CreatedOn], 
				(SELECT '/Pengaduan/PengaduanForm?ID=' + CONVERT(VARCHAR(36), ID) FROM tblT_Dumas WHERE Nomor = A.Nomor) [Msg_Link],
				A.TipePengirim,
				A.DelegatorName				
				 
	FROM		@TABLE A
	GROUP BY	A.Msg_Title, A.Nomor, A.TipePengirim, A.DelegatorName
	
	SELECT * FROM @TABLE_RESULT WHERE Nomor IS NOT NULL ORDER BY s_CreatedOn DESC
	
END