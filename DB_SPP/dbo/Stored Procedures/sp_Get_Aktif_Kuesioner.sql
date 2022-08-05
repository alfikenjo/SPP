








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Aktif_Kuesioner]
	
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @IDKuesioner VARCHAR(36) = (SELECT TOP 1 ID FROM Kuesioner WHERE Status = 'Aktif' AND dbo.FormatDate_yyyyMMdd(StartDate) <= dbo.FormatDate_yyyyMMdd(GETDATE()) AND dbo.FormatDate_yyyyMMdd(EndDate) >= dbo.FormatDate_yyyyMMdd(GETDATE()) ORDER BY ISNULL(UpdatedOn, CreatedOn) DESC)
	--SELECT @IDKuesioner
	
	SELECT	
			@IDKuesioner [IDKuesioner],
			CONVERT(VARCHAR(36), A.ID) [ID], 		
			CONVERT(VARCHAR(36), A.IDHeader) [IDHeader], 		
			A.Num,
			A.InputType,
			A.Label,
			A.Required, 
			ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn],
			CASE 
				WHEN 
				(SELECT LTRIM(RTRIM(X.Options)) + ',' AS 'data()' FROM KuesionerDetailOptions X WHERE X.IDHeader = A.ID ORDER BY X.Num FOR XML PATH('')) <> '' 
					THEN LEFT((SELECT LTRIM(RTRIM(X.Options)) + ',' AS 'data()' FROM KuesionerDetailOptions X WHERE X.IDHeader = A.ID ORDER BY X.Num FOR XML PATH('')), LEN((SELECT LTRIM(RTRIM(X.Options)) + ',' AS 'data()' FROM KuesionerDetailOptions X WHERE X.IDHeader = A.ID ORDER BY X.Num FOR XML PATH(''))) - 1)
				ELSE ''
			END [Options],
			B.Title,
			dbo.FormatDate_yyyyMMdd(B.StartDate) [StartDate],
			dbo.FormatDate_yyyyMMdd(B.EndDate) [EndDate],
			B.Status
	FROM	KuesionerDetail A
			JOIN Kuesioner B ON A.IDHeader = B.ID AND B.ID = @IDKuesioner	
	ORDER BY A.Num ASC


END