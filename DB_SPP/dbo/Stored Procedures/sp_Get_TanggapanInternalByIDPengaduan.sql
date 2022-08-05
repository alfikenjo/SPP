







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_TanggapanInternalByIDPengaduan]
	@IDPengaduan varchar(36),
	@TipePengirim varchar(200)
AS
BEGIN
	--DECLARE @IDPengaduan varchar(36) = '2AAFC020-2224-4DA9-84CB-C1EEAC50D7A5',
	--		@TipePengirim varchar(200) = 'Internal - Delegator'

	
	SELECT	CONVERT(varchar(36), A.ID) [ID],
			A.JenisPengaduan, 
			CONVERT(varchar(36), A.IDPengaduan) [IDPengaduan],
			A.TipePengirim,
			A.Email, A.Nama, A.Tanggapan, A.FileLampiran, A.FileLampiran_Ekstension, A.CreatedBy, 
			
			CASE WHEN @TipePengirim = A.TipePengirim THEN 1 ELSE A.IsRead END [IsRead], 
			dbo.FormatDate_Indonesia_Lengkap(A.CreatedOn) [_Createdon] ,
			
			ISNULL(C.Name, '') [DelegatorName]
	FROM	tblt_Tanggapan A
			JOIN tblT_Dumas B ON A.IDPengaduan = B.ID
			LEFT JOIN tblM_Delegator C ON B.DelegatorID = C.ID
	WHERE	A.TipePengirim IN ('Internal - Admin SPP', 'Internal - Delegator') AND A.IDPengaduan = @IDPengaduan
	ORDER BY A.CreatedOn DESC

	
END