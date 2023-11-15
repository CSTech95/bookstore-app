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

    [HttpGet("User/{userId}")]
    //public IEnumerable<User> GetUsers()
    public User GetSingleUsers(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] FROM BookAppSchema.Users 
                    WHERE userId = "+ userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);

        return user;
    }

     [HttpPost("adduser")]
    public IActionResult AddUser()
    {
        return Ok();
    }

    [HttpPut("edituser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE BookAppSchema.Users
                SET   
                    FirstName = '" + user.FirstName +
                    "', LastName = '" + user.LastName +
                    "', Email = '" + user.Email +
                    "', Gender = '" + user.Gender +
                    "', Active = '" + user.Active +
                "'WHERE userId = " + user.UserId;
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
        throw new Exception("Failed to update user.");
    }
}
