
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE newdilyanka
	AS
BEGIN

	SET NOCOUNT ON;

declare @i int
declare @size float
declare @cost float
declare @owner int
declare @name nchar(10)
declare @type int
declare @typetext nchar(10)

set @i=1000+FLOOR(RAND()*5000)
set @size=1+RAND()*5000
set @cost=@size*5
set @owner=FLOOR(1+RAND()*3)
set @type=FLOOR(1+RAND()*3)
if @owner=1 begin set @name='pasha' end
else if @owner=2 begin set @name='masha' end
else if @owner=3 begin set @name='oleg' end
else if @owner=4 begin set @name='kirill' end

if @type=1 begin set @typetext='type1' end
else if @type=2 begin set @typetext='type2' end
else if @type=3 begin set @typetext='type4' end
else if @type=4 begin set @typetext='type3' end

INSERT INTO [labs].[dbo].[Tablet] VALUES(@i,@name,FLOOR(1+RAND()*8),ROUND(@size,2),ROUND(@cost,2),@typetext);

END
GO
