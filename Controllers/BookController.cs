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

        [HttpGet("Books/{bookId}")]
        public IEnumerable<Book> GetBooks(int bookId)
        {
            string sql = @"EXEC BookAppSchema.spBooks_Get";
            if(bookId != 0){
            sql += " @BookId=" + bookId.ToString();
            }
            IEnumerable<Book> books = _dapper.LoadData<Book>(sql);
            return books;
        }

        [HttpGet("BooksByAuthor/{name}")]
        public IEnumerable<Book> GetBooksByAuthor(string name)
        {
            string getBookByAuthor = @"SELECT [BookId],
                    [BookTitle],
                    [BookAuthorFirstName],
                    [BookAuthorLastName],
                    [Genre],
                    [BookImg],
                    [PublishedYear]
                FROM BookAppSchema.Books
                    WHERE BookAuthorFirstName LIKE '%" + name + "'";

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
                    "','https://placehold.co/600x400@2x.png', GETDATE())";
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
                        string sql = @"EXEC BookAppSchema.spBook_Delete @BookId="+id.ToString();
            
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete book");
        }
    }
}