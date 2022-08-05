






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Export_Dumas_Internal]    
	@Status varchar(200) = null,
	@Username VARCHAR(50)
AS
BEGIN
	
	--DECLARE @Username varchar(200) = 'spp.ptsmi@gmail.com',
	--		@Status varchar(200) = null	
	
	DECLARE @ID_Organisasi VARCHAR(36) = NULL, @Kadar VARCHAR(50) = NULL;	

	IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Email = @Username AND Role IN ('Delegator')) > 0)
	BEGIN		
		SET @ID_Organisasi = (SELECT ID_Organisasi FROM tblM_User WHERE Email = @Username);

		SELECT	ROW_NUMBER() OVER(ORDER BY A.CreatedOn DESC) [No],
				ISNULL(A.Sumber, 'Portal SPP') [Sumber],
				A.Nomor, A.Email [Nama_Pelapor], A.Email, 
				(SELECT DISTINCT X.Nama + '; ' AS 'data()' FROM tblT_Dumas_Detail X WHERE X.ID_Header = A.ID FOR XML PATH('')) [Nama_Terlapor],				
				ISNULL(B.Name, '-') [Delegator],
				dbo.Format24DateTime(A.CreatedOn) [Tanggal_Pengaduan],
				dbo.Format24DateTime(A.WaktuKejadian) [Tanggal_Kejadian],
				A.Status
		
		FROM	tblT_Dumas A
				LEFT JOIN tblM_Delegator B ON A.ID_Organisasi = B.ID
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status
				AND ISNULL(@ID_Organisasi, A.ID_Organisasi) = A.ID_Organisasi
	END	
	ELSE
	BEGIN
		SELECT	ROW_NUMBER() OVER(ORDER BY A.CreatedOn DESC) [No],
				ISNULL(A.Sumber, 'Portal SPP') [Sumber],
				A.Nomor, A.Email [Nama_Pelapor], A.Email, 
				(SELECT DISTINCT X.Nama + '; ' AS 'data()' FROM tblT_Dumas_Detail X WHERE X.ID_Header = A.ID FOR XML PATH('')) [Nama_Terlapor],	
				ISNULL(B.Name, '-') [Delegator],
				dbo.Format24DateTime(A.CreatedOn) [Tanggal_Pengaduan],
				dbo.Format24DateTime(A.WaktuKejadian) [Tanggal_Kejadian],
				A.Status										
		FROM	tblT_Dumas A
				LEFT JOIN tblM_Delegator B ON A.ID_Organisasi = B.ID
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status		
	END
	
	
END