






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Dumas_by_ID]
	@ID varchar(36)
AS
BEGIN
	--DECLARE @ID varchar(36) = (SELECT TOP 1 ID FROM tblT_Dumas ORDER BY CreatedOn DESC)

	SELECT	CONVERT(varchar(36), A.ID) [ID],
			ISNULL(A.Sumber, 'Portal SPP') [Sumber],
			A.Email,
			A.Handphone,
			A.TempatKejadian,
			A.Kronologi,
			A.Status,
			A.Nomor,
			ISNULL(CONVERT(varchar(36), A.DelegatorID), '') [DelegatorID],
			ISNULL(B.Name, '') [DelegatorName],

			ISNULL(A.Jenis_Pelanggaran, '') [Jenis_Pelanggaran],
			ISNULL(A.Keterangan_Penyaluran, '') [Keterangan_Penyaluran],
			A.Keterangan_Penyaluran_Filename, A.Keterangan_Penyaluran_Ekstension,
			--'Direspon oleh Admin SPP: ' + A.PenyaluranBy + ' @' + dbo.FormatDate_Indonesia_Lengkap(A.PenyaluranDate) [PenyaluranByDate],
			A.PenyaluranBy,
			dbo.FormatDate_Indonesia_Lengkap(A.PenyaluranDate) [PenyaluranByDate],

			ISNULL(A.Keterangan_Pemeriksaan, '') [Keterangan_Pemeriksaan],
			A.Keterangan_Pemeriksaan_Filename, A.Keterangan_Pemeriksaan_Ekstension,
			ISNULL(A.Keterangan_Konfirmasi, '') [Keterangan_Konfirmasi],
			A.Keterangan_Konfirmasi_Filename, A.Keterangan_Konfirmasi_Ekstension,
			--'Ditindaklanjuti oleh Grup Delegator: '+ B.Name + ' / ' + A.TindakLanjutBy + ' @' + dbo.FormatDate_Indonesia_Lengkap(A.TindakLanjutDate) [TindakLanjutByDate],
			A.TindakLanjutBy,
			dbo.FormatDate_Indonesia_Lengkap(A.TindakLanjutDate) [TindakLanjutByDate],

			ISNULL(A.Keterangan_Respon, '') [Keterangan_Respon],
			A.Keterangan_Respon_Filename, A.Keterangan_Respon_Ekstension,
			--'Diselesaikan oleh Admin SPP: ' + A.ResponBy + ' @' + dbo.FormatDate_Indonesia_Lengkap(A.ResponDate) [ResponByDate],
			A.ResponBy,
			dbo.FormatDate_Indonesia_Lengkap(A.ResponDate) [ResponByDate],
			
			dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
			dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian],
			dbo.FormatDate_yyyyMMdd(A.WaktuKejadian) [s_WaktuKejadian],
			
			
			(SELECT TOP 1 FileEvidence FROM tblT_File_Evidence WHERE ID_Header = A.ID) [FileEvidence],
			(SELECT TOP 1 FileEvidence_Ekstension FROM tblT_File_Evidence WHERE ID_Header = A.ID) [FileEvidence_Ekstension]
		
	FROM	tblT_Dumas A
			LEFT JOIN tblM_Delegator B ON A.DelegatorID = B.ID
	WHERE	A.ID = @ID 
	
END