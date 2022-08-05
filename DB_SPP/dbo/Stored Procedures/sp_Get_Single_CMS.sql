







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Single_CMS]
	@Tipe VARCHAR(200),
	@Lang VARCHAR(2) = '' 
AS
BEGIN

	SET NOCOUNT ON;
	SELECT CONVERT(VARCHAR(36), A.ID) [ID],
		   CONVERT(VARCHAR(36), A.ID_IN) [ID_IN],		   
		   ISNULL(A.Tipe, '') [Tipe],			
		   ISNULL(A.Lang, '') [Lang],        
		   ISNULL(A.Title, '') [Title],       
		   ISNULL(A.SubTitle, '') [SubTitle],    
		   ISNULL(A.GridTitle, '') [GridTitle],   
		   ISNULL(A.GridContent, '') [GridContent], 
		   ISNULL(A.LabelTombol, '') [LabelTombol], 
		   ISNULL(A.Link, '') [Link],        
		   ISNULL(A.Description, '') [Description], 
		   ISNULL(A.Filename, '') [Filename],    
		   ISNULL(A.Ekstension, '') [Ekstension],  
		   ISNULL(A.Filename1, '') [Filename1],   
		   ISNULL(A.Ekstension1, '') [Ekstension1], 
		   
		   ISNULL(A.Value1, '') [Value1], 
		   ISNULL(A.Value2, '') [Value2], 
		   ISNULL(A.Value3, '') [Value3], 
		   ISNULL(A.Value4, '') [Value4], 
		   ISNULL(A.Value5, '') [Value5], 
		   ISNULL(A.Value6, '') [Value6], 
		   ISNULL(A.Value7, '') [Value7], 
		   ISNULL(A.Value8, '') [Value8], 
		   ISNULL(A.Value9, '') [Value9], 

		   ISNULL(A.Status, '') [Status],
		   ISNULL(ISNULL(A.UpdatedBy, A.CreatedBy), '') [UpdatedBy],
		   dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [s_UpdatedOn]  

	FROM	tblT_CMS A
	WHERE	A.Tipe = @Tipe
			AND A.Lang = ISNULL(NULLIF(@Lang, ''), A.Lang)





END