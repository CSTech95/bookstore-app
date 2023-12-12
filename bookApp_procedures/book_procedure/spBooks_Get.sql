USE BookAppDB
GO

-- EXEC BookAppSchema.spBooks_Get @SearchValue = 'sun'
-- EXEC BookAppSchema.spBooks_Get @BookId=2
CREATE OR ALTER PROCEDURE BookAppSchema.spBooks_Get
    @BookId INT = NULL
    , @SearchValue NVARCHAR(MAX) = NULL
AS
BEGIN
    SELECT [Books].[BookId],
        [Books].[BookTitle],
        [Books].[BookAuthorFirstName],
        [Books].[BookAuthorLastName],
        [Books].[Genre],
        [Books].[BookImg],
        [Books].[PublishedYear] 
    FROM BookAppSchema.Books AS Books
        WHERE Books.BookId = ISNULL(@BookId, BookId)
            AND (@SearchValue IS NULL
            OR Books.BookTitle LIKE '%'+@SearchValue+'%'
            OR Books.BookAuthorFirstName LIKE '%'+@SearchValue+'%'
            OR Books.BookAuthorLastName LIKE '%'+@SearchValue+'%'
            OR Books.BookTitle LIKE '%'+@SearchValue+'%'
            )
END
GO