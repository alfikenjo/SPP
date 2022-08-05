CREATE PROCEDURE [dbo].[sp_Get_Email_Admin_Unit]
	-- Add the parameters for the stored procedure here	
	@IDPengaduan VARCHAR(36)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.	
	SET NOCOUNT ON;

	SELECT	DISTINCT A.Email [Username], A.Fullname, A.Email,
			(SELECT Nomor FROM tblT_Dumas WHERE ID = @IDPengaduan) [Nomor],
			(SELECT Email FROM tblT_Dumas WHERE ID = @IDPengaduan) [Nama],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(CreatedOn) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalKirim]
	FROM	tblM_User A			
			JOIN vw_UserInRole D ON A.UserID = D.UserID AND D.Role = 'Delegator'
	WHERE	A.isActive = 1 AND ISNULL(A.Mail_Verification, 0) = 1
			AND A.Email IS NOT NULL
			--AND A.Notifikasi = 1
			AND A.ID_Organisasi = (SELECT ID_Organisasi FROM tblT_Dumas WHERE ID = @IDPengaduan)
	
		


END