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
                    [BookAuthorFirstName],
                    [BookAuthorLastName],
                    [Genre],
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
                    [BookAuthorFirstName],
                    [BookAuthorLastName],
                    [Genre],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books
                    WHERE BookId =" + id.ToString();

            return _dapper.LoadDataSingle<Book>(sql);
        }

        [HttpGet("BooksByAuthor/{author}")]
        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            string getBookByAuthor = @"SELECT [BookId],
                    [BookTitle],
                    [BookAuthorFirstName],
                    [BookAuthorLastName],
                    [Genre],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books
                    WHERE BookAuthorFirstName LIKE '%" + author + "'";

            Console.WriteLine(getBookByAuthor);

            return _dapper.LoadData<Book>(getBookByAuthor);
        }

        [HttpPost("Book")]
        public IActionResult AddBook(BookAddDto bookToAdd)
        {
            string sql = @"
            INSERT INTO BookAppSchema.Books(
                    [BookTitle],
                    [BookAuthorFirstName],
                    [BookAuthorLastName],
                    [Genre],
                    [BookImg],
                    [PublishedYear]) VALUES (" + 
                    "'" + bookToAdd.BookTitle +
                    "','" + bookToAdd.BookAuthorFirstName +
                    "','" + bookToAdd.BookAuthorLastName +
                    "','" + bookToAdd.Genre +
                    "','" + bookToAdd.BookImg +
                    "', GETDATE())";
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Add book");
        }

        [HttpPut("Book")]
        public IActionResult EditBook(Book bookToEdit)
        {
            string sql = @"
            UPDATE BookAppSchema.Books 
                SET BookTitle = '" + bookToEdit.BookTitle +
                "', BookAuthorFirstName = '" + bookToEdit.BookAuthorFirstName +
                "', BookAuthorLastName = '" + bookToEdit.BookAuthorLastName +
                "', Genre = '" + bookToEdit.Genre +
                "', BookImg = '" + bookToEdit.BookImg +
                "', PublishedYear = '" + bookToEdit.PublishedYear +
                "'WHERE BookId = " + bookToEdit.BookId.ToString();

            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to edit book");
        }

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