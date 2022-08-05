





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Save_KuesionerDetail]
	-- Add the parameters for the stored procedure here
	@ID				varchar(36),
	@IDHeader	    varchar(36),
	@Num			INT,
	@InputType		varchar(50),
	@Label			varchar(MAX),
	@Required		BIT,
	@CreatedBy nvarchar(200)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO KuesionerDetail
	(
		[ID]
		,[IDHeader]
		,[Num]
		,[InputType]
		,[Label]
		,[Required]
		,[CreatedBy]
	)
		VALUES
		(
		@ID,
		@IDHeader,
		@Num,
		@InputType,
		@Label,
		@Required,
		@CreatedBy
		)

	
	
	
END