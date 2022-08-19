CREATE PROCEDURE [dbo].[sp_Get_Email_Admin_Pusat]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	SET NOCOUNT ON;

	SELECT	DISTINCT A.Fullname, A.Email
	FROM	tblM_User A			
			JOIN vw_UserInRole D ON A.UserID = D.UserID AND D.Role = 'Admin SPP'
	WHERE	A.isActive = 1 --AND ISNULL(A.Mail_Verification, 0) = 1
			AND A.Email IS NOT NULL
			AND ISNULL(A.EmailNotification, 0) = 1
	
	
END

--SELECT * FROM tblM_User WHERE Email = 'mh.alfi.syahri@gmail.comx'
--UPDATE tblM_User SET Email = 'spp.ptsmi@gmail.com' WHERE UserID = '4620A4C8-91E8-4A73-906F-5A7DDE277796'