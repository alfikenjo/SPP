







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Kuesioner_Detail_By_ID]
	 @ID VARCHAR(36)
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID], 		
			CONVERT(VARCHAR(36), A.IDHeader) [IDHeader], 		
			A.Num,
			A.InputType,
			A.Label,
			A.Required, 
			ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]

	FROM	KuesionerDetail A	
	WHERE	A.ID = @ID
	ORDER BY A.Num ASC


END