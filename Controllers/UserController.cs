using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
 


    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("Users/")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {

        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] FROM BookAppSchema.Users;";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }
    //[HttpGet("User/{userId}")]
    ////public IEnumerable<User> GetUsers()
    //public User GetSingleUsers(int userId)
    //{
    //    return new User;
    //    //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    //{
    //    //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //    //    TemperatureC = Random.Shared.Next(-20, 55),
    //    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    //})
    //    //.ToArray();
    //}
}
