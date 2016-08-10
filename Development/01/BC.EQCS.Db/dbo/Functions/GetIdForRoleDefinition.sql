
CREATE FUNCTION GetIdForRoleDefinition
(
    @ShortCode VARCHAR(25)
)
    RETURNS INT
AS
BEGIN
    DECLARE @ReturnId INT;
	
	SELECT @ReturnId = [dbo].[ApplicationRole].[Id] 
	FROM [dbo].[ApplicationRole]
	WHERE Code = @ShortCode
      
	RETURN @ReturnId; 

END