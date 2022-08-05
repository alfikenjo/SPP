




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_Banner]
	-- Add the parameters for the stored procedure here
	@Action nvarchar(10),
	
	@ID				varchar(36),

	@Lang			varchar(2),
	@Filename		varchar(255),
	@Ekstension		varchar(50),
	@Title			varchar(100) = '',
	@Title_Color	varchar(20) = '',
	@SubTitle		varchar(150) = '',
	@SubTitle_Color	varchar(20) = '',
	@LabelTombol	varchar(50) = '',
	@Link			varchar(MAX) = '',

	@Status varchar(50),
	@CreatedBy nvarchar(200) = ''
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblT_Banner
		(
			 [ID]
			,[ID_IN]
			,[Lang]
			,[Filename]
			,[Ekstension]
			,[Title]
			,[Title_Color]
			,[SubTitle]
			,[SubTitle_Color]
			,[LabelTombol]
			,[Link]
			,[Status]
			,[CreatedBy]
		)
		 VALUES
		 (
			CASE WHEN @Lang = 'EN' THEN NEWID() ELSE @ID END,
			CASE WHEN @Lang = 'EN' THEN @ID ELSE NULL END,
			@Lang,
			@Filename,
			@Ekstension,
			@Title,
			@Title_Color,
			@SubTitle,
			@SubTitle_Color,
			@LabelTombol,
			@Link,
			@Status,
			@CreatedBy
		 )

		 EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > CMS > Banner', @Lang, NULL, 'INSERT', @Title
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		IF(@Lang = 'ID')
		BEGIN
			UPDATE  tblT_Banner
			SET		Filename = ISNULL(NULLIF(@Filename, ''), Filename),
					Ekstension = ISNULL(NULLIF(@Ekstension, ''), Ekstension),
					Title = @Title,
					Title_Color = @Title_Color,
					SubTitle = @SubTitle,
					SubTitle_Color = @SubTitle_Color,
					LabelTombol = @LabelTombol,
					Link = @Link,
					Status = @Status,
					UpdatedOn = GETDATE(),
					UpdatedBy = @CreatedBy
			WHERE	ID = @ID AND Lang = 'ID';
		END
		ELSE IF(@Lang = 'EN')
		BEGIN
			UPDATE  tblT_Banner
			SET		Filename = ISNULL(NULLIF(@Filename, ''), Filename),
					Ekstension = ISNULL(NULLIF(@Ekstension, ''), Ekstension),
					Title = @Title,
					Title_Color = @Title_Color,
					SubTitle = @SubTitle,
					SubTitle_Color = @SubTitle_Color,
					LabelTombol = @LabelTombol,
					Link = @Link,
					Status = @Status,
					UpdatedOn = GETDATE(),
					UpdatedBy = @CreatedBy
			WHERE	ID_IN = @ID AND Lang = 'EN';
		END

		EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > CMS > Banner', @Lang, NULL, 'UPDATE', @Title
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		IF(SELECT COUNT(*) FROM tblT_Banner WHERE Lang = 'ID') > 1 AND (SELECT COUNT(*) FROM tblT_Banner WHERE Lang = 'EN') > 1
		BEGIN
			DELETE FROM tblT_Banner WHERE ID_IN = @ID;
			DELETE FROM tblT_Banner WHERE ID = @ID;
			EXEC sp_RecordAuditTrail @CreatedBy, 'Sys Administrator > CMS', 'Banner', NULL, 'DELETE', @Title
		END
	END

	IF(@Status = 'Aktif')
	BEGIN
		UPDATE tblT_Banner SET Status = 'Non Aktif' WHERE ID <> @ID AND Lang = 'ID' AND Status = 'Aktif';
		UPDATE tblT_Banner SET Status = 'Non Aktif' WHERE ID_IN <> @ID AND Lang = 'EN' AND Status = 'Aktif';
	END
	
END