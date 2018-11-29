begin transaction work;
INSERT INTO [labs].[dbo].[Tablet] VALUES(11111,'rollback',0,0,0,'shit');
rollback work;