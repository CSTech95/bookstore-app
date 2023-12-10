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

        //Get all Books, Or a Book by Id, or search all Books based on 
        //Book Title, or Author's First or last name
        [HttpGet("Books/{id}/{searchParam}")]
        public IEnumerable<Book> GetBooks(int id=0, string searchParam = "None")
        {
            string sql = @"EXEC BookAppSchema.spBooks_Get";
            string parameters = "";
            if(id != 0)
            {
                parameters += ", @BookId=" + id.ToString();
            }
            if(searchParam != "None")
            {
                parameters += ", @searchValue='" + searchParam+"'";
            }

            if (parameters.Length>0)
            {   
                sql += parameters.Substring(1); 
            }
            return _dapper.LoadData<Book>(sql);
        }

        [HttpPut("Book")]
        public IActionResult AddBook(BookAddDto bookToAdd)
        {
            string sql = @"EXEC BookAppSchema.spBooks_Upsert
                               @BookTitle = '"+bookToAdd.BookTitle +
                               "', @BookAuthorFirstName= '"+bookToAdd.BookAuthorFirstName +
                               "', @BookAuthorLastName= '"+bookToAdd.BookAuthorLastName + 
                               "', @Genre= '"+bookToAdd.Genre +
                               "', @BookImg= '"+bookToAdd.BookImg +
                               "', @PublishedYear= '"+bookToAdd.PublishedYear+ "'";

            if(bookToAdd.BookId>0){
                sql += ", @BookId = " + bookToAdd.BookId;
            }
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            Console.WriteLine(sql);
            throw new Exception("Failed to Add book");
        }

        //[HttpPut("Book")]
        //public IActionResult EditBook(Book bookToEdit)
        //{
        //    string sql = @"
        //    UPDATE BookAppSchema.Books 
        //        SET BookTitle = '" + bookToEdit.BookTitle +
        //        "', BookAuthorFirstName = '" + bookToEdit.BookAuthorFirstName +
        //        "', BookAuthorLastName = '" + bookToEdit.BookAuthorLastName +
        //        "', Genre = '" + bookToEdit.Genre +
        //        "', BookImg = '" + bookToEdit.BookImg +
        //        "', PublishedYear = '" + bookToEdit.PublishedYear +
        //        "'WHERE BookId = " + bookToEdit.BookId.ToString();

        //    if(_dapper.Execute(sql))
        //    {
        //        return Ok();
        //    }
        //    throw new Exception("Failed to edit book");
        //}

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