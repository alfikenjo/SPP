






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Detail_PengaduanByIDHeader]
    @ID_Header varchar(36)
AS
BEGIN
	--DECLARE @Email varchar(200) = 'mh.alfi.syahri@gmail.com',
	--@Status varchar(200) = null

	SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
			A.Nama,
			A.NomorHandphone,
			A.Departemen,
			A.Jabatan,
			A.FileIdentitas,
			A.FileIdentitas_Ekstension
		
	FROM	tblT_Dumas_Detail A
			
	WHERE	A.ID_Header = @ID_Header
			
	
END