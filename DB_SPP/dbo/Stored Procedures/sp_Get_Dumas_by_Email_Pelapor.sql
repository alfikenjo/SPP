



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Dumas_by_Email_Pelapor]
    @Email varchar(200),
	@Status varchar(200) = null
AS
BEGIN
	--DECLARE @Email varchar(200) = 'larva.opat@gmail.com',
	--@Status varchar(200) = null

	--SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Email AND [Role] = 'Delegator'

	IF(@Status = 'Diproses')
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				A.Nomor, A.Email, 
				CASE 
					WHEN A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut') THEN 'Diproses'
					WHEN A.Status IN ('Terkirim') THEN 'Terkirim'
					WHEN A.Status IN ('Selesai') THEN 'Selesai'
					WHEN A.Status IN ('Ditolak Admin SPP') THEN 'Ditolak'
				END [Status],

				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut')
				AND A.Email = @Email AND A.Nomor IS NOT NULL
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(@Status = 'Terkirim')
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				A.Nomor, A.Email, 
				CASE 
					WHEN A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut') THEN 'Diproses'
					WHEN A.Status IN ('Terkirim') THEN 'Terkirim'
					WHEN A.Status IN ('Selesai') THEN 'Selesai'
					WHEN A.Status IN ('Ditolak Admin SPP') THEN 'Ditolak'
				END [Status],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	A.Status IN ('Terkirim')
				AND A.Email = @Email AND A.Nomor IS NOT NULL
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(@Status = 'Selesai')
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				A.Nomor, A.Email, 
				CASE 
					WHEN A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut') THEN 'Diproses'
					WHEN A.Status IN ('Terkirim') THEN 'Terkirim'
					WHEN A.Status IN ('Selesai') THEN 'Selesai'
					WHEN A.Status IN ('Ditolak Admin SPP') THEN 'Ditolak'
				END [Status],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	A.Status IN ('Selesai')
				AND A.Email = @Email AND A.Nomor IS NOT NULL
		ORDER BY A.CreatedOn DESC
	END
	ELSE IF(@Status = 'Ditolak')
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				A.Nomor, A.Email, 
				CASE 
					WHEN A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut') THEN 'Diproses'
					WHEN A.Status IN ('Terkirim') THEN 'Terkirim'
					WHEN A.Status IN ('Selesai') THEN 'Selesai'
					WHEN A.Status IN ('Ditolak Admin SPP') THEN 'Ditolak'
				END [Status],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	A.Status IN ('Ditolak Admin SPP')
				AND A.Email = @Email AND A.Nomor IS NOT NULL
		ORDER BY A.CreatedOn DESC
	END
	ELSE
	BEGIN
		SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
				A.Nomor, A.Email, 
				CASE 
					WHEN A.Status IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut') THEN 'Diproses'
					WHEN A.Status IN ('Terkirim') THEN 'Terkirim'
					WHEN A.Status IN ('Selesai') THEN 'Selesai'
					WHEN A.Status IN ('Ditolak Admin SPP') THEN 'Ditolak'
				END [Status],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
				(SELECT TOP 1 Nama FROM tblT_Dumas_Detail WHERE ID_Header = A.ID) [NamaTerlapor],
				ISNULL((SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = A.ID)), '') [DelegatorName],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Pelapor' AND IsRead = 0) [Unread_Tanggapan_Pelapor],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Admin SPP' AND IsRead = 0) [Unread_Tanggapan_Internal_Admin_SPP],
				(SELECT COUNT(*) FROM tblT_Tanggapan WHERE IDPengaduan = A.ID AND TipePengirim = 'Internal - Delegator' AND IsRead = 0) [Unread_Tanggapan_Internal_Delegator]
		
		FROM	tblT_Dumas A
				
		WHERE	A.Email = @Email AND A.Nomor IS NOT NULL
		ORDER BY A.CreatedOn DESC
	END
END