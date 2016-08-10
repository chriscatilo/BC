CREATE FUNCTION [stage].[CleanLocationName]
(
	@name varchar(500),
	@town varchar(500)
)
RETURNS VARCHAR(500) 
AS
BEGIN

	set @name = REPLACE(@name, 'The British Council -', '')

	set @name = REPLACE(@name, 'The British Council', '')
	
	set @name = REPLACE(@name, 'British Council,', '')

	set @name = REPLACE(@name, 'British Council - ', '')
	
	set @name = REPLACE(@name, 'British Council-', '')

	set @name = REPLACE(@name, 'British Council', '')
	
	set @name = REPLACE(@name, '.', '')
	
	set @name = LTRIM(RTRIM(@name))
	
	if (@name = '') set @name = @town;
	
	return dbo.TitleCase(@name);  
END

