






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Banner]
	 
AS
BEGIN

	SET NOCOUNT ON;
	
	--DECLARE @TABLE AS TABLE 
	--		(
	--			ID VARCHAR(36), ID_IN VARCHAR(36), Lang VARCHAR(10), 
	--			Filename_ID VARCHAR(200), Ekstension_ID VARCHAR(100), Title_ID VARCHAR(100), SubTitle_ID VARCHAR(150), LabelTombol_ID VARCHAR(100), Link_ID VARCHAR(MAX), 
	--			Filename_EN VARCHAR(200), Ekstension_EN VARCHAR(100), Title_EN VARCHAR(100), SubTitle_EN VARCHAR(150), LabelTombol_EN VARCHAR(100), Link_EN VARCHAR(MAX),		
	--			Status VARCHAR(50), UpdatedBy VARCHAR(200), s_UpdatedOn VARCHAR(100)
	--		)

	--INSERT INTO @TABLE (ID, Lang, Filename_ID, Ekstension_ID, Title_ID, SubTitle_ID, LabelTombol_ID, Link_ID, Status, UpdatedBy, s_UpdatedOn)
	SELECT	CONVERT(VARCHAR(36), A.ID) [ID], 		
			A.Lang,
			A.Filename, 
			A.Ekstension,
			A.Title,
			A.SubTitle,
			A.LabelTombol,
			A.Link,	
			A.Status,
			ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [s_UpdatedOn]

	FROM	tblT_Banner A
	WHERE	A.Lang = 'ID'
	ORDER BY ISNULL(A.UpdatedOn, A.CreatedOn) DESC

	--UPDATE  B SET 
	--		B.ID = CONVERT(VARCHAR(36), A.ID), 
	--		B.ID_IN = ISNULL(CONVERT(VARCHAR(36), A.ID_IN), ''),
	--		B.Lang = A.Lang, 
	--		B.Filename_EN = A.Filename, 
	--		B.Ekstension_EN = A.Ekstension, 
	--		B.Title_EN = A.Title, 
	--		B.SubTitle_EN = A.SubTitle, 
	--		B.LabelTombol_EN = A.LabelTombol, 
	--		B.Link_EN = A.Link, 
	--		B.Status = A.Status, 
	--		B.UpdatedBy = ISNULL(A.UpdatedBy, A.CreatedBy), 
	--		B.s_UpdatedOn = dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn))

	--FROM	tblT_Banner A
	--		JOIN @TABLE B ON A.ID_IN = B.ID AND B.Lang = 'ID' AND A.Lang = 'EN'

	--SELECT * FROM @TABLE

END