



--EXEC sp_Get_User

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Download_User]
	@isActive VARCHAR(2) = 2,
	@Roles VARCHAR(36) = '',
	@CreatedBy VARCHAR(30),
	@Tipe VARCHAR(50)
AS
BEGIN

	--DECLARE @isActive VARCHAR(2),
	--		@Roles VARCHAR(MAX),
	--		@Tipe VARCHAR(50) = 'Download PDF',
	--		@CreatedBy VARCHAR(30)
	--SET		@isActive = 2;
	--SET		@Roles = '';
	
	SET NOCOUNT ON;

	EXEC sp_RecordAuditTrail @CreatedBy, 'User Management', 'Daftar User', NULL, @Tipe, NULL
		
	DECLARE @TABLE AS TABLE (UserID UNIQUEIDENTIFIER, Fullname VARCHAR(MAX), Mobile VARCHAR(MAX), Email VARCHAR(MAX), NIP VARCHAR(MAX), Jabatan VARCHAR(MAX), isActive INT, Roles VARCHAR(MAX), s_UpdatedOn VARCHAR(MAX), UpdatedOn DATETIME, UpdatedBy VARCHAR(MAX), s_Status VARCHAR(MAX), Status VARCHAR(MAX), Img VARCHAR(100), Delegators VARCHAR(MAX));

	DECLARE @QUERY VARCHAR(MAX);

	SET @QUERY = 'SELECT	A.UserID, A.Fullname, 
						dbo.Format_StringNumber(A.Mobile) [Mobile], 
						A.Email, ISNULL(A.NIP, ''''), ISNULL(A.Jabatan, ''''),
						A.isActive,
						ISNULL((SELECT DISTINCT B.Name + ''; '' AS ''data()'' FROM tblT_UserInRole X LEFT JOIN tblM_Role B ON X.RoleID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('''')), '''') [Roles],
						dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [s_UpdatedOn],
						A.UpdatedOn,
						ISNULL(A.UpdatedBy, A.CreatedBy) [UpdatedBy],
						CASE WHEN A.isActive = 1 THEN ''Aktif'' ELSE ''Non-Aktif'' END [Status],
						CASE WHEN A.isActive = 1 THEN ''Aktif'' ELSE ''Non Aktif'' END [s_Status],
						A.Img,
						ISNULL((SELECT DISTINCT B.Name + ''; '' AS ''data()'' FROM tblT_UserInDelegator X LEFT JOIN tblM_Delegator B ON X.DelegatorID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('''')), '''') [Delegatosr]
				FROM	tblM_User A 							
				WHERE	ISNULL(A.IsDeleted, 0) <> 1';

	IF(@isActive <> '2')
	BEGIN
		SET @QUERY = @QUERY + ' AND A.isActive = '+ @isActive +'';
	END				


	
	IF(LEN(@Roles) > 1)
	BEGIN
		
		INSERT INTO @TABLE
		EXECUTE (@QUERY);

		SELECT  Fullname, Mobile, Email, NIP, Jabatan, isActive, SUBSTRING(Roles, 1, 254) [Roles], s_UpdatedOn [UpdatedOn], UpdatedBy, s_Status [Status], Delegators
		FROM	@TABLE 
		WHERE	Roles LIKE '%'+ @Roles +'%' 				
	END	
	ELSE
	BEGIN
	
		INSERT INTO @TABLE
		EXECUTE (@QUERY);

		SELECT	Fullname, Mobile, Email, NIP, Jabatan, isActive, SUBSTRING(Roles, 1, 254) [Roles], s_UpdatedOn [UpdatedOn], UpdatedBy, s_Status [Status], Delegators
		FROM	@TABLE 		
	END	
	
	--INSERT INTO @TABLE
	--EXECUTE (@QUERY);
	--SELECT * FROM @TABLE --WHERE ISNULL(Unit, '') = ISNULL(NULLIF(@UnitAdmin, ''), ISNULL(Unit, ''))

	--SELECT @Query
END