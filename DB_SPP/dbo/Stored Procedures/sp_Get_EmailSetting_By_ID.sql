








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_EmailSetting_By_ID]
	@ID VARCHAR(36)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID],
		    ISNULL(A.Tipe, '') [Tipe],
			ISNULL(A.Subject, '') [Subject],
			ISNULL(A.Konten, '') [Konten],
			ISNULL(A.Status, '') [Status],
			B.Parameter,
		   ISNULL(ISNULL(A.UpdatedBy, A.CreatedBy), '') [UpdatedBy],
		   dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]  

	FROM	tblT_EmailSetting A
			LEFT JOIN tblT_EmailMatriks B ON A.Tipe = B.Tipe
	
	WHERE	A.ID = @ID



END