CREATE PROCEDURE BookAppSchema.spBook_Delete
@BookId INT
AS
BEGIN
    DELETE FROM BookAppSchema.Books WHERE BookId = @BookId
END
GO