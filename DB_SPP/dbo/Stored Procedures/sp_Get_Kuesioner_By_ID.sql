







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Kuesioner_By_ID]
	 @ID VARCHAR(36)
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID], 		
			A.Title,
			dbo.FormatDate_yyyyMMdd(A.StartDate) [StartDate],
			dbo.FormatDate_yyyyMMdd(A.EndDate) [EndDate],
			A.Status, 
			ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]

	FROM	Kuesioner A	
	WHERE	A.ID = @ID
	ORDER BY ISNULL(A.UpdatedOn, A.CreatedOn) DESC


END