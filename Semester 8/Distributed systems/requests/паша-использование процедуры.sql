USE [labs]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[newdilyanka]

SELECT	'Return Value' = @return_value

GO
