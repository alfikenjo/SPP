






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_KuesionerDetailOptions]
	-- Add the parameters for the stored procedure here	
	@ID				varchar(36),
	@IDHeader	    varchar(36),
	@Num			INT,
	@Options		varchar(MAX),
	@CreatedBy nvarchar(MAX)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	INSERT INTO KuesionerDetailOptions
		(
			 [ID]
			,[IDHeader]
			,[Num]
			,[Options]
			,[CreatedBy]
		)
		 VALUES
		 (
			@ID,
			@IDHeader,
			@Num,
			LTRIM(RTRIM(@Options)),
			@CreatedBy
		 )

	
	
END