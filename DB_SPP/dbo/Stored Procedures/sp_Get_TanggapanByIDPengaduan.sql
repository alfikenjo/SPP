






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_TanggapanByIDPengaduan]
	@IDPengaduan varchar(36)
AS
BEGIN
	--DECLARE @IDPengaduan varchar(36) = '4D9582DF-3DE5-41AF-9F61-0847EFDC82FA'
	
	SELECT	CONVERT(varchar(36), ID) [ID],
			JenisPengaduan, 
			CONVERT(varchar(36), IDPengaduan) [IDPengaduan],
			TipePengirim,
			Email, Nama, Tanggapan, FileLampiran, FileLampiran_Ekstension, CreatedBy, IsRead, 
			dbo.FormatDate_Indonesia_Lengkap(CreatedOn) [_Createdon]  
	FROM	tblt_Tanggapan 
	WHERE	TipePengirim IN ('Pelapor', 'Admin SPP') AND IDPengaduan = @IDPengaduan
	ORDER BY CreatedOn DESC

	
END