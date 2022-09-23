
CREATE PROCEDURE [dbo].[GetUserByEmailId]
	@emailId NCHAR(100)
	AS
	BEGIN
	SELECT *
	FROM [dbo].[Users]
	WHERE EmailAddress = @emailId
	END