





-- =============================================
-- Author:		<Author,,UserID>
-- Create date: <Create Date,,>
-- DelegatorID:	<DelegatorID,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_MemberDelegator]
	-- Add the parameters for the stored procedure here
	 @Action nvarchar(10),
	 @ID varchar(36),
     @UserID varchar(36) = '',
     @DelegatorID varchar(36) = '',
	 @CreatedBy nvarchar(200) = ''
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Email_Audit VARCHAR(200) = (SELECT Email FROM tblM_User WHERE UserID = @UserID)
	DECLARE @Audit VARCHAR(MAX) = (SELECT Name FROM tblM_Delegator WHERE ID = @DelegatorID)

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblT_UserInDelegator
			   (
				[ID]
			   ,[UserID]
			   ,[DelegatorID]
			   ,[CreatedBy])
		 VALUES
		 (
				@ID,
			    @UserID,
			    @DelegatorID,	
				@CreatedBy
		 )

		 DELETE A FROM tblT_UserInRole A JOIN tblM_Role B ON A.RoleID = B.ID AND B.Name = 'Delegator' AND A.UserID = @UserID;
		 INSERT INTO tblT_UserInRole (UserID, RoleID, CreatedBy)
		 SELECT TOP 1 @UserID, ID, @CreatedBy FROM tblM_Role WHERE Name = 'Delegator'
		 
		
		 EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Member Delegator', @Audit, 'ADD', @Email_Audit
	END
	
	ELSE IF(@Action = 'hapus')
	BEGIN	
	declare @countgroup  UNIQUEIDENTIFIER = (select top 1 DelegatorID from tblT_UserInDelegator where CONVERT(VARCHAR(36), id) = @ID)
	declare @countuser int = (select count (DISTINCT UserID) from tblT_UserInDelegator where DelegatorID = @countgroup group by delegatorID having COUNT(*) > 1)
	if @countuser > 1 
	begin
		DECLARE @OldUserID UNIQUEIDENTIFIER = (SELECT TOP 1 UserID FROM tblT_UserInDelegator WHERE ID = @ID)

		IF(SELECT COUNT(*) FROM tblT_UserInDelegator WHERE UserID = @OldUserID) = 1
		BEGIN
			DELETE A FROM tblT_UserInRole A JOIN tblM_Role B ON A.RoleID = B.ID AND B.Name = 'Delegator' AND A.UserID = @OldUserID;
		END
		DELETE FROM tblT_UserInDelegator WHERE ID = @ID;

		EXEC sp_RecordAuditTrail @CreatedBy, 'Admin SPP', 'Member Delegator', @Audit, 'DELETE', @Email_Audit
	END
	END
	
END