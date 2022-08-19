






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Get_Dashboard_By_Email]
	@Email VARCHAR(MAX) = ''
AS
BEGIN

	--DECLARE @Email VARCHAR(MAX) = 'alfi.kenjo@gmail.com'

	DECLARE @TABLE AS TABLE (Terkirim INT, Diproses INT, Selesai INT, Ditolak INT)

	INSERT INTO @TABLE VALUES (0,0,0,0)

	UPDATE @TABLE SET Terkirim = (SELECT COUNT(*) [Terkirim] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND Email = @Email);
	UPDATE @TABLE SET Diproses = (SELECT COUNT(*) [Diproses] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND Email = @Email AND [Status] IN ('Diproses', 'Ditolak Delegator', 'Dihentikan', 'Ditindak lanjut'));
	UPDATE @TABLE SET Selesai = (SELECT COUNT(*) [Selesai] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND Email = @Email AND [Status] = 'Selesai') 
	UPDATE @TABLE SET Ditolak = (SELECT COUNT(*) [Ditolak] FROM tblT_Dumas WHERE Nomor IS NOT NULL AND Email = @Email AND [Status] IN ('Ditolak', 'Ditolak Admin SPP') )

	SELECT * FROM @TABLE
	
END