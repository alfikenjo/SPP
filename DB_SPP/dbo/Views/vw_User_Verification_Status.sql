





CREATE VIEW [dbo].[vw_User_Verification_Status]
AS

SELECT	A.ID, B.UserID, B.Fullname, A.Email, LEFT(A.Status, 8) [V_Status],
		CASE 
			WHEN (DATEDIFF(hour, A.UpdatedOn, GETDATE())) > 24 THEN 'Expired'			
			ELSE 'Active'
		END [Link_Status]
FROM	tblT_New_User_Verification A 
		JOIN tblM_User B ON A.UserID = B.UserID