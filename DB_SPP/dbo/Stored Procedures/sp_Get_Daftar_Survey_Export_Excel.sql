



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Daftar_Survey_Export_Excel]
	@JenisPengaduan VARCHAR(200) = ''
AS
BEGIN

	SELECT	ROW_NUMBER() OVER(ORDER BY A.CreatedOn DESC) [No]			
			,A.NamaSurvey
			,A.JenisPengaduan
			,A.NamaPengirim
			,A.Umur
			,A.Gender
			,A.Pendidikan
			,A.Pekerjaan
			,A.Email
			,A.Handphone		
			,dbo.Format24DateTime(A.CreatedOn) [CreatedOn]
			,A.CreatedBy

			,A.Jawaban_1 [Prosedur Pelayanan]
			,A.Jawaban_2 [Persyaratan Layanan]
			,A.Jawaban_3 [Waktu Pelayanan]
			,A.Jawaban_4 [Bbiaya Layanan]
			,A.Jawaban_5 [Hasil Pelayanan]
			,A.Jawaban_6 [Keamanan Pelayanan]
			,A.Jawaban_7 [Kemudahan Menyampaikan Pengaduan]

	FROM	tblT_Survey A
			JOIN tblT_Dumas B ON A.IDPengaduan = B.ID
			
	WHERE	A.JenisPengaduan = @JenisPengaduan
END