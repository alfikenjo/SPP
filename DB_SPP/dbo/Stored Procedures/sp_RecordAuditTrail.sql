






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_RecordAuditTrail]
	@Username VARCHAR(50) = '',
	@Menu VARCHAR(MAX) = '',
	@Halaman VARCHAR(MAX) = '',
	@Item VARCHAR(MAX) = '',
	@Action VARCHAR(MAX) = '',
	@Description VARCHAR(MAX) = ''
AS
BEGIN
	
	INSERT INTO AuditTrail (Username, Menu, Halaman, Item, [Action], [Description])
	VALUES (
				NULLIF(@Username, ''),
				NULLIF(@Menu, ''),
				NULLIF(@Halaman, ''),
				NULLIF(@Item, ''),
				NULLIF(@Action, ''),
				NULLIF(@Description, '')
		   )

	
END