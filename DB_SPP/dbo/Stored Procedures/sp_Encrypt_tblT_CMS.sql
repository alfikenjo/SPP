﻿



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Encrypt_tblT_CMS]
	@ID			VARCHAR(36),
	@CreatedBy  VARCHAR(MAX) NULL,
	@UpdatedBy  VARCHAR(MAX) NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [tblT_CMS]
	SET 
			[CreatedBy] =  NULLIF(@CreatedBy, '')
			,[UpdatedBy] =  NULLIF(@UpdatedBy, '')
			

	 WHERE [ID] = @ID

	

END