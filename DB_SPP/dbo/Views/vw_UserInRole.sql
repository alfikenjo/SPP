








CREATE VIEW [dbo].[vw_UserInRole]
AS

SELECT  A.ID, A.UserID, B.Fullname, C.ID [RoleID], C.Name [Role], B.Email,
		A.CreatedBy, dbo.Format24DateTime(A.CreatedOn) [CreatedOn], B.isActive
FROM	tblT_UserInRole A
		JOIN tblM_User B ON A.UserID = B.UserID
		JOIN tblM_Role C ON A.RoleID = C.ID		
WHERE	ISNULL(B.IsDeleted, 0) = 0