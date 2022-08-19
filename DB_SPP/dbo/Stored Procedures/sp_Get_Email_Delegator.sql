CREATE PROCEDURE [dbo].[sp_Get_Email_Delegator]
	-- Add the parameters for the stored procedure here	
	@IDPengaduan VARCHAR(36)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.	

	--DECLARE @IDPengaduan VARCHAR(36) = (SELECT TOP 1  ID FRom tblT_Dumas ORDER BY CreatedOn DESC)

	SET NOCOUNT ON;

	DECLARE @DelegatorID VARCHAR(36) = (SELECT DelegatorID FROM tblT_Dumas WHERE ID = @IDPengaduan);

	SELECT	DISTINCT A.Email [Username], A.Fullname, A.Email,
			(SELECT Nomor FROM tblT_Dumas WHERE ID = @IDPengaduan) [Nomor],
			(SELECT Email FROM tblT_Dumas WHERE ID = @IDPengaduan) [EmailPelapor],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(CreatedOn) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalKirim],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(PenyaluranDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalPenyaluran],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(TindakLanjutDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalTindakLanjut],
			(SELECT dbo.FormatDate_Indonesia_Lengkap(ResponDate) FROM tblT_Dumas WHERE ID = @IDPengaduan) [TanggalRespon],
			B.Name [DelegatorName]
	FROM	tblM_User A			
			JOIN vw_UserInRole D ON A.UserID = D.UserID AND D.Role = 'Delegator'
			JOIN tblT_UserInDelegator C ON A.UserID = C.UserID 
			JOIN tblM_Delegator B ON C.DelegatorID = B.ID AND B.ID = @DelegatorID
	WHERE	A.isActive = 1 --AND ISNULL(A.Mail_Verification, 0) = 1
			AND A.Email IS NOT NULL
			AND ISNULL(A.EmailNotification, 0) = 1
			
	
END