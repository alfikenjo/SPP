





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_CMS]
	-- Add the parameters for the stored procedure here
	@Action				VARCHAR(10),
	@ID					VARCHAR(36),
	@Tipe				VARCHAR(255),  
	@Lang               VARCHAR(2),    
	@Title              VARCHAR(255),  
	@SubTitle           VARCHAR(255),  
	@GridTitle          VARCHAR(255),  
	@GridContent        VARCHAR(MAX),  
	@LabelTombol        VARCHAR(50),   
	@Link               VARCHAR(MAX),  
	@Description        VARCHAR(MAX),  
	@Filename           VARCHAR(255),  
	@Ekstension         VARCHAR(50),   
	@Filename1          VARCHAR(255),  
	@Ekstension1        VARCHAR(50),   

	@Value1				VARCHAR(MAX),  
	@Value2				VARCHAR(MAX),  
	@Value3				VARCHAR(MAX),  
	@Value4				VARCHAR(MAX),  
	@Value5				VARCHAR(MAX),  
	@Value6				VARCHAR(MAX),  
	@Value7				VARCHAR(MAX),  
	@Value8				VARCHAR(MAX),  
	@Value9				VARCHAR(MAX),  

	@Status             VARCHAR(50),   
	@CreatedBy          VARCHAR(200) 
		 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Entity VARCHAR(200) = 'Sys Administrator > CMS > '+ @Tipe;

	IF(@Action = 'add')
	BEGIN
		INSERT INTO tblT_CMS
		(
			ID			,	
			ID_IN		,	
			Tipe		,	
			Lang        ,    
			Title       ,    
			SubTitle    ,    
			GridTitle   ,    
			GridContent ,    
			LabelTombol ,    
			Link        ,    
			Description ,    
			Filename    ,    
			Ekstension  ,    
			Filename1   ,    
			Ekstension1 ,
			
			Value1		,
			Value2		,
			Value3		,
			Value4		,
			Value5		,
			Value6		,
			Value7		,
			Value8		,
			Value9		,
			    
			Status      ,    
			CreatedBy       
		)
		 VALUES
		 (
			CASE WHEN @Lang = 'EN' THEN NEWID() ELSE @ID END,
			CASE WHEN @Lang = 'EN' THEN @ID ELSE NULL END,
			@Tipe				, 
			@Lang               , 
			@Title              , 
			@SubTitle           , 
			@GridTitle          , 
			@GridContent        , 
			@LabelTombol        , 
			@Link               , 
			@Description        , 
			@Filename           , 
			@Ekstension         , 
			@Filename1          , 
			@Ekstension1        , 

			@Value1				,
			@Value2				,
			@Value3				,
			@Value4				,
			@Value5				,
			@Value6				,
			@Value7				,
			@Value8				,
			@Value9				,

			@Status             , 
			@CreatedBy          
		 )

		 
		 EXEC sp_RecordAuditTrail @CreatedBy, @Entity, @Lang, NULL, 'INSERT', @ID
	END
	ELSE IF(@Action = 'edit')
	BEGIN
		IF(@Lang = 'ID')
		BEGIN
			UPDATE  tblT_CMS
			SET		
					Title       = @Title      ,    
					SubTitle    = @SubTitle   ,    
					GridTitle   = @GridTitle  ,    
					GridContent = @GridContent,    
					LabelTombol = @LabelTombol,    
					Link        = @Link       ,    
					Description = @Description,    
					Filename = ISNULL(NULLIF(@Filename, ''), Filename),
					Ekstension = ISNULL(NULLIF(@Ekstension, ''), Ekstension),
					Filename1 = ISNULL(NULLIF(@Filename1, ''), Filename1),
					Ekstension1 = ISNULL(NULLIF(@Ekstension1, ''), Ekstension1),
					
					Value1 = @Value1,
					Value2 = @Value2,
					Value3 = @Value3,
					Value4 = @Value4,
					Value5 = @Value5,
					Value6 = @Value6,
					Value7 = @Value7,
					Value8 = @Value8,
					Value9 = @Value9,

					Status      = @Status,    
					UpdatedOn = GETDATE(),
					UpdatedBy = @CreatedBy
			WHERE	ID = @ID AND Lang = 'ID';
		END
		ELSE IF(@Lang = 'EN')
		BEGIN
			UPDATE  tblT_CMS
			SET		
					Title       = @Title      ,    
					SubTitle    = @SubTitle   ,    
					GridTitle   = @GridTitle  ,    
					GridContent = @GridContent,    
					LabelTombol = @LabelTombol,    
					Link        = @Link       ,    
					Description = @Description,    
					Filename = ISNULL(NULLIF(@Filename, ''), Filename),
					Ekstension = ISNULL(NULLIF(@Ekstension, ''), Ekstension),
					Filename1 = ISNULL(NULLIF(@Filename1, ''), Filename1),
					Ekstension1 = ISNULL(NULLIF(@Ekstension1, ''), Ekstension1),

					Value1 = @Value1,
					Value2 = @Value2,
					Value3 = @Value3,
					Value4 = @Value4,
					Value5 = @Value5,
					Value6 = @Value6,
					Value7 = @Value7,
					Value8 = @Value8,
					Value9 = @Value9,

					Status      = @Status,    
					UpdatedOn = GETDATE(),
					UpdatedBy = @CreatedBy
			WHERE	ID_IN = @ID AND Lang = 'EN';
		END

		EXEC sp_RecordAuditTrail @CreatedBy, @Entity, @Lang, NULL, 'UPDATE', @ID
	END
	ELSE IF(@Action = 'hapus')
	BEGIN
		IF(SELECT COUNT(*) FROM tblT_CMS WHERE Lang = 'ID') > 1 AND (SELECT COUNT(*) FROM tblT_CMS WHERE Lang = 'EN') > 1
		BEGIN
			DELETE FROM tblT_CMS WHERE ID_IN = @ID;
			DELETE FROM tblT_CMS WHERE ID = @ID;
			EXEC sp_RecordAuditTrail @CreatedBy, @Entity, @Lang, NULL, 'DELETE', @ID
		END
	END

	IF(@Status = 'Aktif')
	BEGIN
		UPDATE tblT_CMS SET Status = 'Non Aktif' WHERE ID <> @ID AND Lang = 'ID' AND Status = 'Aktif' AND Tipe = @Tipe;
		UPDATE tblT_CMS SET Status = 'Non Aktif' WHERE ID_IN <> @ID AND Lang = 'EN' AND Status = 'Aktif' AND Tipe = @Tipe;
	END
	
END