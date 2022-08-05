






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_KuesionerValue]
	-- Add the parameters for the stored procedure here
	@ID				varchar(36),
	@IDPengaduan	varchar(36),
	@Title			varchar(MAX),
	@Num			INT,
	@InputType		varchar(50),
	@Label			varchar(MAX),
	@Required		BIT,
	@Options		varchar(MAX),
	@InputValue		varchar(MAX),
	@CreatedBy	    nvarchar(200)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO KuesionerValue
	(
		[ID]
		,[IDPengaduan]
		,[Title]
		,[Num]
		,[InputType]
		,[Label]
		,[Required]
		,[Options]
		,[InputValue]
		,[CreatedBy]
	)
		VALUES
		(
		@ID,
		@IDPengaduan,
		@Title,
		@Num,
		@InputType,
		@Label,
		@Required,
		@Options,
		@InputValue,
		@CreatedBy
		)

	
	
	
END