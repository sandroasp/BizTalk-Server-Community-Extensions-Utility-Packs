CREATE PROCEDURE [dbo].[BizTalkCRMLookup] 
    @guidField varchar(38),
	@entityName varchar(200),
	@keyField varchar(100),
	@valueIn varchar(100)
AS 
BEGIN

    SET NOCOUNT ON;

	DECLARE @sqlstr	VARCHAR(1000)
	
    SET @sqlstr = 'SELECT ' 
					+ @guidField 
					+ ' FROM ' 
					+ @entityName  
					+ ' WHERE ' 
					+ @keyField + ' = ''' + REPLACE(@valueIn, '''', '''''') + ''''
	
	EXEC (@sqlstr)
	
END
