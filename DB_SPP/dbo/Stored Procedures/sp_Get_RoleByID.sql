






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_RoleByID]
	-- Add the parameters for the stored procedure here
	@ID VARCHAR(36)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--DECLARE @ID VARCHAR(36);
	--SET @ID = '9873c60c-385f-4916-a079-15cc20101db1'

	SELECT	CONVERT(VARCHAR(36), ID) [ID], Name, Description, Status

	FROM	tblM_Role
	WHERE	ID = @ID
END