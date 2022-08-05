







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Banner_Aktif]
	 @Lang VARCHAR(20) = 'ID'
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @TABLE AS TABLE 
			(
				ID VARCHAR(36), ID_IN VARCHAR(36), Lang VARCHAR(10), 
				Filename_ID VARCHAR(200), Ekstension_ID VARCHAR(100), Title_ID VARCHAR(100), Title_ID_Color VARCHAR(20), SubTitle_ID VARCHAR(150), SubTitle_ID_Color VARCHAR(20), LabelTombol_ID VARCHAR(100), Link_ID VARCHAR(MAX), 
				Filename_EN VARCHAR(200), Ekstension_EN VARCHAR(100), Title_EN VARCHAR(100), Title_EN_Color VARCHAR(20), SubTitle_EN VARCHAR(150), SubTitle_EN_Color VARCHAR(20), LabelTombol_EN VARCHAR(100), Link_EN VARCHAR(MAX),		
				Status VARCHAR(50), UpdatedBy VARCHAR(200), s_UpdatedOn VARCHAR(100)
			)

	--INSERT INTO @TABLE (ID, Lang, Filename_ID, Ekstension_ID, Title_ID, Title_ID_Color, SubTitle_ID, SubTitle_ID_Color, LabelTombol_ID, Link_ID, Status, UpdatedBy, s_UpdatedOn)
	SELECT	TOP 1 CONVERT(VARCHAR(36), A.ID) [ID], 		
			A.Lang,
			A.Filename, 
			A.Ekstension,
			A.Title,
			CASE WHEN A.Title_Color = 'Light' THEN '#fff' ELSE '#444' END [Title_Color],
			A.SubTitle,
			CASE WHEN A.SubTitle_Color = 'Light' THEN '#fff' ELSE '#555' END [SubTitle_Color],
			A.Status

	FROM	tblT_Banner A
	WHERE	A.Status = 'Aktif' AND A.Lang = @Lang

	

END