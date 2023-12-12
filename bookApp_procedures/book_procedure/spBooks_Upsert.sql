CREATE OR ALTER PROCEDURE BookAppSchema.spBooks_Upsert
    @BookTitle NVARCHAR(100)
    , @BookAuthorFirstName NVARCHAR(50)
    , @BookAuthorLastName NVARCHAR(50)
    , @Genre NVARCHAR(30)
    , @BookImg NVARCHAR(255)
    , @PublishedYear NVARCHAR(4)
    , @BookId INT = NULL

AS
BEGIN
    IF NOT EXISTS (SELECT * FROM BookAppSchema.Books WHERE BookId = @BookId)
        BEGIN
            INSERT INTO BookAppSchema.Books(
                [BookTitle],
                [BookAuthorFirstName],
                [BookAuthorLastName],
                [Genre],
                [BookImg],
                [PublishedYear]
            ) VALUES (
                @BookTitle,
                @BookAuthorFirstName,
                @BookAuthorLastName,
                @Genre,
                @BookImg,
                @PublishedYear
            )
        END
    ELSE
        BEGIN
            UPDATE BookAppSchema.Books 
                SET BookTitle = @BookTitle,
                    BookAuthorFirstName = @BookAuthorFirstName,
                    BookAuthorLastName = @BookAuthorLastName,
                    Genre = @Genre,
                    BookImg = @BookImg,
                    PublishedYear = @PublishedYear
                WHERE BookId = @BookId
        END
END
GO
