



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Database_Pelapor]
	@isActive VARCHAR(2) = '',
	@Email VARCHAR(MAX) = '',
	@Status VARCHAR(200) = '',
	@Jenis VARCHAR(200) = ''
AS
BEGIN

	--DECLARE @isActive VARCHAR(2) = '',
	--@Email VARCHAR(MAX) = '',
	--@Status VARCHAR(200) = '',
	--@Jenis VARCHAR(200) = 'Dumas'


	IF(ISNULL(NULLIF(@Status, ''), '') <> '')
	BEGIN
		SELECT	ROW_NUMBER() OVER(ORDER BY A.Fullname ASC) [No],
				A.Email [Fullname], 
				A.Email,
				A.Mobile, 				
				dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn],
				ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
				CASE WHEN A.isActive = 1 THEN 'Aktif' ELSE 'Non-Aktif' END [Status],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE CreatedBy = A.Email AND Nomor IS NOT NULL ) [Count_Pengaduan]			
		FROM	tblM_User A
		WHERE	ISNULL(NULLIF(@isActive, ''), A.isActive) = A.isActive			
				AND ISNULL(NULLIF(@Email, ''), A.Email) = A.Email				
				AND (SELECT COUNT(*) FROM vw_UserInRole WHERE A.UserID = UserID AND [Role] = 'Pelapor') > 0
				AND (SELECT COUNT(*) FROM tblT_Dumas WHERE CreatedBy = A.Email AND Nomor IS NOT NULL AND Status = @Status ) > 0				
				AND (SELECT COUNT(*) FROM tblT_Dumas WHERE CreatedBy = A.Email AND Nomor IS NOT NULL ) > 0
	END
	ELSE 
	BEGIN
		SELECT	ROW_NUMBER() OVER(ORDER BY A.Fullname ASC) [No],
				A.Email [Fullname], 
				A.Email,
				A.Mobile, 				
				dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn],
				ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
				CASE WHEN A.isActive = 1 THEN 'Aktif' ELSE 'Non-Aktif' END [Status],
				(SELECT COUNT(*) FROM tblT_Dumas WHERE CreatedBy = A.Email AND Nomor IS NOT NULL ) [Count_Pengaduan]			
		FROM	tblM_User A
		WHERE	ISNULL(NULLIF(@isActive, ''), A.isActive) = A.isActive			
				AND ISNULL(NULLIF(@Email, ''), A.Email) = A.Email				
				AND (SELECT COUNT(*) FROM vw_UserInRole WHERE A.UserID = UserID AND [Role] = 'Pelapor') > 0				
				AND (SELECT COUNT(*) FROM tblT_Dumas WHERE CreatedBy = A.Email AND Nomor IS NOT NULL ) > 0
				
	END	
END