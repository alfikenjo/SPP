



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Daftar_Survey]
	@JenisPengaduan VARCHAR(200) = ''
AS
BEGIN

	SELECT	ROW_NUMBER() OVER(ORDER BY A.CreatedOn DESC) [No]
			,A.IDPengaduan
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

	FROM	tblT_Survey A
			JOIN tblT_Dumas B ON A.IDPengaduan = B.ID
			
	WHERE	A.JenisPengaduan = @JenisPengaduan
END