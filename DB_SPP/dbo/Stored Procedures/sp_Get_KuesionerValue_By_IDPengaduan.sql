








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_KuesionerValue_By_IDPengaduan]
	 @IDPengaduan VARCHAR(36)
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID], 		
			CONVERT(VARCHAR(36), A.IDPengaduan) [IDPengaduan], 	
			A.Title,	
			A.Num,
			A.InputType,
			A.Label,
			A.Required, 
			A.Options,
			A.InputValue,
			ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]			
	FROM	KuesionerValue A			
	WHERE	A.IDPengaduan = @IDPengaduan
	ORDER BY A.Num ASC


END