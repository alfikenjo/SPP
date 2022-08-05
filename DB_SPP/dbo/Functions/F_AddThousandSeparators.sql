
CREATE FUNCTION [dbo].[F_AddThousandSeparators](@NumStr varchar(50)) 
RETURNS Varchar(50)
AS
BEGIN
declare @OutStr varchar(50)
declare @i int
declare @run int

Select @i=CHARINDEX('.',@NumStr)
if @i=0 
    begin
    set @i=LEN(@NumStr)
    Set @Outstr=''
    end
else
    begin   
     Set @Outstr=SUBSTRING(@NUmStr,@i,50)
     Set @i=@i -1
    end 


Set @run=0

While @i>0
    begin
      if @Run=3
        begin
          Set @Outstr=','+@Outstr
          Set @run=0
        end
      Set @Outstr=SUBSTRING(@NumStr,@i,1) +@Outstr  
      Set @i=@i-1
      Set @run=@run + 1     
    end

    RETURN REPLACE(@OutStr, '.00', '')

END