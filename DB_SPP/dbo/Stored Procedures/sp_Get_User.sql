



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_User]
	@isActive VARCHAR(2),	
	@Roles VARCHAR(MAX) = '',
	@DelegatorID VARCHAR(36) = ''
AS
BEGIN

	--DECLARE @isActive VARCHAR(2),
	--		@Roles VARCHAR(MAX),
	--		@DelegatorID VARCHAR(36) = '';

	--SET		@isActive = 2;
	--SET		@Roles = '';
	
	SET NOCOUNT ON;
	
	DECLARE @TABLE AS TABLE (UserID VARCHAR(36), Fullname VARCHAR(MAX), Mobile VARCHAR(MAX), Email VARCHAR(MAX), NIP VARCHAR(MAX), Jabatan VARCHAR(MAX), Divisi VARCHAR(MAX), isActive INT, Roles VARCHAR(MAX), s_UpdatedOn VARCHAR(MAX), UpdatedBy VARCHAR(MAX), Status VARCHAR(MAX), Img VARCHAR(MAX), Ekstension VARCHAR(MAX), Delegators VARCHAR(MAX));

	DECLARE @QUERY VARCHAR(MAX);

	SET @QUERY = 'SELECT	A.UserID, ISNULL(A.Fullname, '''') [Fullname], 
						dbo.Format_StringNumber(A.Mobile) [Mobile], 
						A.Email, A.NIP, A.Jabatan, A.Divisi,
						A.isActive,
						(SELECT DISTINCT B.Name + ''; '' AS ''data()'' FROM tblT_UserInRole X LEFT JOIN tblM_Role B ON X.RoleID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('''')) [Roles],
						dbo.Format24DateTime(ISNULL(A.UpdatedOn, A.CreatedOn)) [UpdatedOn],
						ISNULL(A.UpdatedBy, A.CreatedBy),
						CASE WHEN A.isActive = 1 THEN ''Aktif'' ELSE ''Non-Aktif'' END [Status],
						Img, Ekstension,
						(SELECT DISTINCT B.Name + ''; '' AS ''data()'' FROM tblT_UserInDelegator X LEFT JOIN tblM_Delegator B ON X.DelegatorID = B.ID WHERE A.UserID = X.UserID FOR XML PATH('''')) [Delegators]
				FROM	tblM_User A											
				WHERE	ISNULL(IsDeleted, 0) = 0';

	IF(@isActive <> '2')
	BEGIN
		SET @QUERY = @QUERY + ' AND A.isActive = '+ @isActive +'';
	END	

	IF(ISNULL(NULLIF(@DelegatorID, ''), '') <> '')
	BEGIN
		SET @QUERY = @QUERY + ' AND A.UserID IN (SELECT UserID FROM tblT_UserInDelegator WHERE CONVERT(VARCHAR(36), DelegatorID) = '''+ @DelegatorID +''')';
	END	

	--SELECT @QUERY;

	IF(LEN(@Roles) > 1)
	BEGIN
		INSERT INTO @TABLE
		EXECUTE (@QUERY);

		SELECT  UserID, Fullname, Mobile, Email, NIP, Jabatan, isActive, SUBSTRING(Roles, 1, 254) [Roles], s_UpdatedOn, UpdatedBy, Status, Img, Ekstension, Delegators 
		FROM	@TABLE WHERE Roles LIKE '%'+ @Roles +'%';
	END	
	ELSE
	BEGIN
		INSERT INTO @TABLE
		EXECUTE (@QUERY);
		SELECT * FROM @TABLE;
	END	
	
END