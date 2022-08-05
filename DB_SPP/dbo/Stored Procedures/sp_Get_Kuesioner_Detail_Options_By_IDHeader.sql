







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Kuesioner_Detail_Options_By_IDHeader]
	 @IDHeader VARCHAR(36)
AS
BEGIN

	SET NOCOUNT ON;
	
	--SELECT	CONVERT(VARCHAR(36), A.ID) [ID], 		
	--		CONVERT(VARCHAR(36), A.IDHeader) [IDHeader], 		
	--		A.Num,
	--		LTRIM(RTRIM(A.Options)) [Options],
	--		ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
	--		dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn]

	--FROM	KuesionerDetailOptions A	
	--WHERE	A.IDHeader = @IDHeader
	--ORDER BY A.Num ASC
	DECLARE @Options VARCHAR(MAX) = (SELECT LTRIM(RTRIM(X.Options)) + ',' AS 'data()' FROM KuesionerDetailOptions X WHERE X.IDHeader = @IDHeader ORDER BY X.Num FOR XML PATH(''))
	IF(LEN(@Options) > 0)
	BEGIN
		SET @Options = LEFT(@Options, LEN(@Options) - 1) 
	END

	SELECT @Options [Options]


END