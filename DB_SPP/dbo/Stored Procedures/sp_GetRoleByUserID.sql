






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetRoleByUserID]
	@UserID VARCHAR(36)
AS
BEGIN

	SET NOCOUNT ON;
	
	--DECLARE @UnitAdmin VARCHAR(MAX); 
	--SELECT @UnitAdmin = B.Name FROM tblM_User A JOIN tblM_Unit B ON A.ID_Unit = B.ID WHERE A.UserID = @UserID;

	IF((SELECT COUNT(*) FROM vw_UserInRole WHERE UserID = @UserID AND Role = 'System Administrator') > 0)
	BEGIN
		SELECT	CONVERT(varchar(36), ID) [ID]
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
	ELSE 
	BEGIN
		SELECT	CONVERT(varchar(36), ID) [ID]
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
		WHERE	Name = 'Admin SPP'
		ORDER BY Name
	END
	
END