






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Delegator]
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), ID) [ID]
			,Name
			,Description
			
			,isActive

			,CASE WHEN ISNULL(isActive, 0) = 1 THEN 'Aktif' ELSE 'Non Aktif' END [Status]
			,(SELECT COUNT(*) FROM tblT_UserInDelegator D JOIN tblM_User B ON D.UserID = B.UserID AND ISNULL(B.isDeleted, 0) = 0 WHERE D.DelegatorID = A.ID) [CountMember]
			,CreatedOn
			,CreatedBy
			,UpdatedOn
			,ISNULL(UpdatedBy, CreatedBy) [UpdatedBy]
			,dbo.Format24DateTime(ISNULL(UpdatedOn, CreatedOn)) [s_UpdatedOn]	
	FROM	tblM_Delegator A
	ORDER BY Name
END