





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Dumas_by_Email]
    @Email VARCHAR(MAX),
	@Status varchar(200) = null,
	@Jenis_Pelanggaran varchar(max) = null
AS
BEGIN
	--DECLARE @Email VARCHAR(MAX) = 'spp.ptsmi@gmail.com',
	--@Status varchar(200) = null,
	--@Jenis_Pelanggaran varchar(max) = null

	DECLARE @UserID UNIQUEIDENTIFIER = (SELECT TOP 1 UserID FROM vw_UserInRole WHERE Email = @Email AND [Role] IN ('Delegator'))
	--SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] = 'Delegator'

	IF(SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] = 'Admin SPP') > 0
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				ISNULL(A.Sumber, 'Portal SPP') [Sumber],
				A.Nomor, A.Email, 
				A.Status,
				ISNULL(A.Jenis_Pelanggaran, '') [Jenis_Pelanggaran],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				--(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL(B.Name, '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				LEFT JOIN tblM_Delegator B ON A.DelegatorID = B.ID
			
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status								
				AND ISNULL(NULLIF(@Jenis_Pelanggaran, ''), ISNULL(A.Jenis_Pelanggaran, '')) = ISNULL(A.Jenis_Pelanggaran, '')
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] = 'Delegator') > 0
	BEGIN
	   
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				ISNULL(A.Sumber, 'Portal SPP') [Sumber],
				A.Nomor, A.Email, 
				A.Status,
				ISNULL(A.Jenis_Pelanggaran, '') [Jenis_Pelanggaran],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				--(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				JOIN tblM_Delegator B ON A.DelegatorID = B.ID
				JOIN tblT_UserInDelegator C ON B.ID = C.DelegatorID AND C.UserID = @UserID
				
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status
				AND ISNULL(NULLIF(@Jenis_Pelanggaran, ''), ISNULL(A.Jenis_Pelanggaran, '')) = ISNULL(A.Jenis_Pelanggaran, '')
		ORDER BY A.CreatedOn DESC		

	END	
	ELSE IF(SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] = 'Pelapor') > 0
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				ISNULL(A.Sumber, 'Portal SPP') [Sumber],
				A.Nomor, A.Email, 
				A.Status,
				ISNULL(A.Jenis_Pelanggaran, '') [Jenis_Pelanggaran],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				--(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status
				AND ISNULL(NULLIF(@Jenis_Pelanggaran, ''), ISNULL(A.Jenis_Pelanggaran, '')) = ISNULL(A.Jenis_Pelanggaran, '')
				AND A.CreatedBy = @Email
		ORDER BY A.CreatedOn DESC
	END	
	
END