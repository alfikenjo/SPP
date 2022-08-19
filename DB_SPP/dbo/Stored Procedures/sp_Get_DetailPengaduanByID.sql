






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_DetailPengaduanByID]
    @ID varchar(36)
AS
BEGIN
	--DECLARE @Email VARCHAR(MAX) = 'mh.alfi.syahri@gmail.com',
	--@Status varchar(200) = null

	SELECT	CONVERT(VARCHAR(36), A.ID) [ID],	
			CONVERT(VARCHAR(36), A.ID_Header) [ID_Header],
			A.Nama,
			A.NomorHandphone,
			A.Departemen,
			A.Jabatan
		
	FROM	tblT_Dumas_Detail A
			
	WHERE	A.ID  = @ID 
			
	
END