





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Dumas_Internal]    
	@Status varchar(200) = null,
	@Username VARCHAR(200)
AS
BEGIN
	
	--DECLARE @Username varchar(200) = 'spp.ptsmi@gmail.com',
	--		@Status varchar(200) = 'Terkirim'	
	
	DECLARE @ID_Organisasi VARCHAR(36) = NULL;	

	IF((SELECT COUNT(*) FROM vw_UserInRole WHERE Username = @Username AND Role IN ('Delegator')) > 0)
	BEGIN		

		SELECT	A.*,
				ISNULL(B.Name, 'Tidak Diketahui') [s_UnitKerja],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian]				
		
		FROM	tblT_Dumas A
				LEFT JOIN tblM_Delegator B ON A.ID_Organisasi = B.ID
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status
				AND A.ID_Organisasi IN (SELECT DISTINCT DelegatorID FROM tblT_UserInDelegator WHERE UserID = (SELECT UserID FROM vw_UserInRole WHERE Username = @Username AND Role IN ('Delegator')))
	END	
	ELSE
	BEGIN
		SELECT	A.*,
				ISNULL(B.Name, 'Tidak Diketahui') [s_UnitKerja],
				dbo.Format24DateTime(A.CreatedOn) [_CreatedOn],
				dbo.Format24DateTime(A.WaktuKejadian) [_WaktuKejadian]
		
		FROM	tblT_Dumas A
				LEFT JOIN tblM_Delegator B ON A.ID_Organisasi = B.ID
		WHERE	ISNULL(NULLIF(@Status, ''), A.Status) = A.Status		
	END
	
	
END