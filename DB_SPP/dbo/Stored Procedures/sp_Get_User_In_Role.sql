
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_User_In_Role]
	-- Add the parameters for the stored procedure here
	@Username VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	C.Name [Role]
	FROM	tblM_User A
			JOIN tblT_UserInRole B ON A.UserID = B.UserID
			JOIN tblM_Role C ON B.RoleID = C.ID			

	WHERE	A.Username = @Username			
			AND A.isActive = 1 AND C.Status = '1'
	
END