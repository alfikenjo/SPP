






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_FileEvidenceByIDHeader]
    @ID_Header varchar(36)
AS
BEGIN
	--DECLARE @Email varchar(200) = 'mh.alfi.syahri@gmail.com',
	--@Status varchar(200) = null

	SELECT	CONVERT(VARCHAR(36), A.ID) [ID],	
			CONVERT(VARCHAR(36), A.ID_Header) [ID_Header],
			A.FileEvidence, A.FileEvidence_Ekstension
		
	FROM	tblT_File_Evidence A
			
	WHERE	A.ID_Header = @ID_Header
			
	
END