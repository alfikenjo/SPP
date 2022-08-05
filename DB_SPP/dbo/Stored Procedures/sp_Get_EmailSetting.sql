








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_EmailSetting]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
		    ISNULL(A.Tipe, '') [Tipe],
			ISNULL(A.Subject, '') [Subject],
			ISNULL(A.Konten, '') [Konten],
			ISNULL(A.Status, '') [Status],
		   ISNULL(ISNULL(A.UpdatedBy, A.CreatedBy), '') [UpdatedBy],
		   dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]  

	FROM	tblT_EmailSetting A
	




END