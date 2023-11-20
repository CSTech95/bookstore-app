using BookStoreApp.Data;
using BookStoreApp.Dtos;
using BookStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController: ControllerBase
    {
        private readonly DataContextDapper _dapper;
        public BookController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Books")]
        public IEnumerable<Book> GetBooks()
        {
            string sql = @"SELECT [BookId],
                    [BookTitle],
                    [BookAuthor],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books;";

            return _dapper.LoadData<Book>(sql);
        }

        [HttpGet("Book/{id}")]
        public Book GetSingleBookById(int id)
        {
            string sql = @"SELECT [BookId],
                    [BookTitle],
                    [BookAuthor],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books
                    WHERE BookId =" + id.ToString();

            return _dapper.LoadDataSingle<Book>(sql);
        }
        [HttpGet("BooksByAuthor/{author}")]
        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            string sql = @"SELECT [BookId],
                    [BookTitle],
                    [BookAuthor],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books
                    WHERE BookId =" + author;

            return _dapper.LoadData<Book>(sql);
        }

        //TODO
        //[HttpPost("Rental")]
        //public IActionResult AddBook(RentalAddDto rentalToAdd)
        //{
        //    string sql = @"
        //    INSERT INTO BookAppSchema.Rentals(
        //        [UserId],
        //        [BookId],
        //        [StartDate],
        //        [EndDate]) VALUES (" + this.User.FindFirst("userId")?.Value
        //        + ", '" + rentalToAdd.BookId
        //        + "', GETDATE(), GETDATE()+8 )";
        //    if(_dapper.Execute(sql))
        //    {
        //        return Ok();
        //    }
        //    throw new Exception("Failed to create rental");
        //}

        //TODO
        //[HttpPut("Book")]
        //public IActionResult EditBook(RentalEditDto rentalToEdit)
        //{
        //    string sql = @"
        //    UPDATE BookAppSchema.Rentals 
        //    SET EndDate = GETDATE()+8
        //    WHERE RentalId = " + rentalToEdit.RentalId.ToString() +
        //    "AND UserId = " + this.User.FindFirst("userId")?.Value;

        //    if(_dapper.Execute(sql))
        //    {
        //        return Ok();
        //    }
        //    throw new Exception("Failed to edit rental");
        //}

        [HttpDelete("Book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            string sql = @"DELETE FROM BookAppSchema.Books
                WHERE BookId = " + id.ToString()+
                    "AND UserId = " + this.User.FindFirst("userId")?.Value;;
            
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete book");
        }
    }
}