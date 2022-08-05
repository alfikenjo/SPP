








-- =============================================
-- Author:		<Author,,Nama>
-- Create date: <Create Date,,>
-- Deskripsi:	<Deskripsi,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAccountByUserID]
	-- Add the parameters for the stored procedure here
	@UserID VARCHAR(36)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--DECLARE @UserID VARCHAR(36) = (SELECT UserID FROM tblM_User WHERE Username = 'devin.mela')


	SELECT	C.Name [Role]
	FROM	tblM_User A
			JOIN tblT_UserInRole B ON A.UserID = B.UserID
			JOIN tblM_Role C ON B.RoleID = C.ID			

	WHERE	A.UserID = @UserID
			AND A.isActive = 1 AND C.Status = '1'

	
END