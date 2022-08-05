



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_User_By_UserID]
	@UserID VARCHAR(36)
AS
BEGIN

	--DECLARE @UserID VARCHAR(36) = '4620A4C8-91E8-4A73-906F-5A7DDE277796'

	SET NOCOUNT ON;
	
	SELECT	CONVERT(VARCHAR(36), A.UserID) [UserID], A.Fullname, 
			ISNULL(A.Mobile, A.MobileTemp) [Mobile], 
			A.Email, A.NIP, A.Jabatan, A.Divisi, A.Gender, A.Address,
			A.isActive, A.EmailNotification, A.Mobile_Verification, ISNULL(A.MobileTemp, '') [MobileTemp],
			(SELECT DISTINCT B.Name + '; ' AS 'data()' FROM tblT_UserInRole X LEFT JOIN tblM_Role B ON X.RoleID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('')) [Roles],
			(SELECT DISTINCT CONVERT(VARCHAR(36), B.ID) + '; ' AS 'data()' FROM tblT_UserInRole X LEFT JOIN tblM_Role B ON X.RoleID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('')) [ID_Roles],

			(SELECT DISTINCT B.Name + '; ' AS 'data()' FROM tblT_UserInDelegator X LEFT JOIN tblM_Delegator B ON X.DelegatorID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('')) [Delegators],

			dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [s_UpdatedOn],
			A.UpdatedBy,
			CASE WHEN A.isActive = 1 THEN 'Aktif' ELSE 'Non-Aktif' END [Status],
			Img, Notifikasi
	FROM	tblM_User A 
	WHERE	A.UserID = @UserID			

	
END