/****** Сценарий для команды SelectTopNRows среды SSMS  ******/
SELECT TOP 1000 [id]
      ,[owner]
      ,[quality]
      ,[size]
      ,[cost]
      ,[form of own]
  FROM [labs].[dbo].[Tablet]
  WHERE size>1000