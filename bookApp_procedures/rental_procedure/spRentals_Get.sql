USE BookAppDB
GO

CREATE OR ALTER PROCEDURE BookAppSchema.spRentals_Get
@RentalId INT = NULL,
@UserId INT = NULL,
@SearchValue DATETIME = NULL
AS
BEGIN
    SELECT [Rentals].[RentalId],
        [Rentals].[UserId],
        [Rentals].[BookId],
        [Rentals].[StartDate],
        [Rentals].[EndDate] 
    FROM BookAppSchema.Rentals AS Rentals
    WHERE Rentals.UserId = ISNULL(UserId, Rentals.UserId)
        AND Rentals.RentalId = ISNULL(@RentalId, RentalId)
            AND (@SearchValue IS NULL
            OR Rentals.StartDate LIKE @SearchValue
            OR Rentals.EndDate LIKE @SearchValue
            )
END
GO