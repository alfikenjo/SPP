




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_MemberDelegator]
	@DelegatorID VARCHAR(36) = ''
AS
BEGIN

	--DECLARE @DelegatorID VARCHAR(36) = (SELECT TOP 1 ID FROM tblM_Delegator)

	SET NOCOUNT ON;
	
	DECLARE @TABLE AS TABLE (UserID UNIQUEIDENTIFIER, Fullname VARCHAR(150), Mobile VARCHAR(100), Email VARCHAR(100), NIP VARCHAR(100), Jabatan VARCHAR(100), isActive INT, Roles VARCHAR(MAX), s_UpdatedOn VARCHAR(50), UpdatedBy VARCHAR(30), Status VARCHAR(20), Img VARCHAR(100), Delegators VARCHAR(MAX));
	
	SELECT	CONVERT(VARCHAR(36), B.ID) [ID], 
			CONVERT(VARCHAR(36), A.UserID) [UserID], 
			A.Fullname, 
			dbo.Format_StringNumber(A.Mobile) [Mobile], 
			A.Email,
			dbo.Format24DateTime(B.CreatedOn) [s_UpdatedOn],
			B.CreatedBy,
			A.Img,
			A.Ekstension,
			(SELECT Name FROM tblM_Delegator WHERE ID = @DelegatorID) [DelegatorName]
			--(SELECT DISTINCT B.Name + '; ' AS 'data()' FROM tblT_UserInDelegator X LEFT JOIN tblM_Delegator B ON X.DelegatorID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('')) [Delegators]
	FROM	tblM_User A
			JOIN tblT_UserInDelegator B ON A.UserID = B.UserID AND B.DelegatorID = @DelegatorID							
	
	
	
	
END