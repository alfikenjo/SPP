





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Role]
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), ID) [ID]
			,Name
			,Description
			,ISNULL(Privileges, '') [Privileges]
			,Status

			,CASE WHEN ISNULL(Status, 0) = 1 THEN 'Aktif' ELSE 'Non Aktif' END [s_Status]
			,CreatedOn
			,CreatedBy
			,UpdatedOn
			,ISNULL(UpdatedBy, CreatedBy) [UpdatedBy]
			,dbo.Format24DateTime(ISNULL(UpdatedOn, CreatedOn)) [s_UpdatedOn]	
	FROM	tblM_Role
	ORDER BY Name
END