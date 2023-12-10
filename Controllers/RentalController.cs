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
    public class RentalController: ControllerBase
    {
        private readonly DataContextDapper _dapper;
        public RentalController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Rentals/{id}/{searchParam}")]
        public IEnumerable<Rental> GetRentals(int id=0, string searchParam="None")
        {
            string sql = @"EXEC BookAppSchema.spRentals_Get";
            string parameters = "";
            if(id != 0)
            {
                parameters += ", @RentalId=" + id.ToString();
            }
            if(searchParam != "None")
            {
                parameters += ", @searchValue='" + searchParam+"'";
            }

            if (parameters.Length>0)
            {   
                sql += parameters.Substring(1); 
            }
            //string sql = @"SELECT [RentalId],
            //        [UserId],
            //        [BookId],
            //        [StartDate],
            //        [EndDate] 
            //    FROM BookAppSchema.Rentals;";

            return _dapper.LoadData<Rental>(sql);
        }

        [HttpGet("Rental/{id}")]
        public Rental GetSingleRental(int id)
        {
            string sql = @"SELECT [RentalId],
                    [UserId],
                    [BookId],
                    [StartDate],
                    [EndDate] 
                FROM BookAppSchema.Rentals
                    WHERE RentalId =" + id.ToString();

            return _dapper.LoadDataSingle<Rental>(sql);
        }
        [HttpGet("RentalsOfUser/{uid}")]
        public IEnumerable<Rental> GetRentalsOfUser(int uid)
        {
            string sql = @"SELECT [RentalId],
                    [UserId],
                    [BookId],
                    [StartDate],
                    [EndDate] 
                FROM BookAppSchema.Rentals
                    WHERE UserId =" + uid.ToString();

            return _dapper.LoadData<Rental>(sql);
        }

        [HttpGet("MyRentals")]
        public IEnumerable<Rental> GetMyRentals()
        {
            string sql = @"SELECT [RentalId],
                    [UserId],
                    [BookId],
                    [StartDate],
                    [EndDate] 
                FROM BookAppSchema.Rentals
                    WHERE UserId =" + this.User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Rental>(sql);
        }

        [HttpPost("Rental")]
        public IActionResult AddRental(RentalAddDto rentalToAdd)
        {
            string sql = @"
            INSERT INTO BookAppSchema.Rentals(
                [UserId],
                [BookId],
                [StartDate],
                [EndDate]) VALUES (" + this.User.FindFirst("userId")?.Value
                + ", '" + rentalToAdd.BookId
                + "', GETDATE(), GETDATE()+8 )";
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to create rental");
        }

        [HttpPut("Rental")]
        public IActionResult EditRental(RentalEditDto rentalToEdit)
        {
            string sql = @"
            UPDATE BookAppSchema.Rentals 
            SET EndDate = GETDATE()+8
            WHERE RentalId = " + rentalToEdit.RentalId.ToString() +
            "AND UserId = " + this.User.FindFirst("userId")?.Value;

            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to edit rental");
        }

        [HttpDelete("Rental/{id}")]
        public IActionResult DeleteRental(int id)
        {
            string sql = @"DELETE FROM BookAppSchema.Rentals
                WHERE RentalId = " + id.ToString()+
                    "AND UserId = " + this.User.FindFirst("userId")?.Value;;
            
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete rental");
        }
    }
}