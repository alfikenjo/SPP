




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_CalonMemberDelegator]
	@DelegatorID VARCHAR(36) = ''
AS
BEGIN

	--DECLARE @DelegatorID VARCHAR(36) = NEWID();

	SET NOCOUNT ON;
	
	DECLARE @TABLE AS TABLE (UserID UNIQUEIDENTIFIER, Fullname VARCHAR(150), Mobile VARCHAR(100), Email VARCHAR(100), NIP VARCHAR(100), Jabatan VARCHAR(100), isActive INT, Roles VARCHAR(MAX), s_UpdatedOn VARCHAR(50), UpdatedBy VARCHAR(30), Status VARCHAR(20), Img VARCHAR(100), Delegators VARCHAR(MAX));
	
	SELECT	CONVERT(VARCHAR(36), A.UserID) [UserID], 
			A.Fullname, 
			dbo.Format_StringNumber(A.Mobile) [Mobile], 
			A.Email,		
			A.Img,
			ISNULL((SELECT DISTINCT B.Name + '; ' AS 'data()' FROM tblT_UserInDelegator X JOIN tblM_Delegator B ON X.DelegatorID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('')), '') [Delegators],
			(SELECT Name FROM tblM_Delegator WHERE ID = @DelegatorID) [DelegatorName],
			CASE WHEN A.isActive = 1 THEN 'Aktif' ELSE 'Non-Aktif' END [Status]
	FROM	tblM_User A												
	WHERE	A.isActive = 1
			AND (SELECT COUNT(*) FROM vw_UserInRole WHERE UserID = A.UserID AND Role IN ('Pelapor', 'Admin SPP', 'System Administrator')) = 0
			AND (SELECT COUNT(*) FROM tblT_UserInDelegator WHERE UserID = A.UserID AND DelegatorID = @DelegatorID) = 0
		


	
	
	
END