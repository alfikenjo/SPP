
CREATE PROCEDURE [dbo].[sp_Get_Email_Admin_SPP]
	-- Add the parameters for the stored procedure here	
	@IDPengaduan VARCHAR(36)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.	

	--DECLARE @IDPengaduan VARCHAR(36) = (SELECT TOP 1  ID FRom tblT_Dumas ORDER BY CreatedOn DESC)

	SET NOCOUNT ON;

	DECLARE @DelegatorID VARCHAR(36) = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan);

	SELECT	DISTINCT A.Fullname, A.Email,
			(SELECT Nomor FROM tblT_Dumas WHERE ID = @IDPengaduan) [Nomor],
			(SELECT Email FROM tblT_Dumas WHERE ID = @IDPengaduan) [EmailPelapor],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(CreatedOn) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalKirim],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(PenyaluranDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalPenyaluran],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(TindakLanjutDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalTindakLanjut],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(ResponDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalRespon],
			(SELECT Name FROM tblM_Delegator WHERE ID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan)) [DelegatorName]
	FROM	tblM_User A			
			JOIN vw_UserInRole D ON A.UserID = D.UserID AND D.Role = 'Admin SPP'			
	WHERE	A.isActive = 1 --AND ISNULL(A.Mail_Verification, 0) = 1
			AND A.Email IS NOT NULL
			AND ISNULL(A.EmailNotification, 0) = 1
			
	
		--(SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan)
		--(SELECT COUNT(*) FROM tblT_UserInDelegator WHERE UserID = '81548F93-E8C5-4BB2-B81C-0FDF727AA806' AND DelegatorID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan))
		--(SELECT UserID FROM tblT_UserInDelegator WHERE DelegatorID = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan))
END


--UPDATE tblT_Dumas SET DelegatorID = (SELECT TOP 1 ID FROM tblM_Delegator) WHERE ID = 'E4E19D98-2782-488F-A119-5DE403A3C340'