using BookStoreApp.Data;
using BookStoreApp.Dtos;
using BookStoreApp.Models;
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

    [HttpGet("Users")]
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

    [HttpGet("User/{Id}")]
    public User GetSingleUser(int Id)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] FROM BookAppSchema.Users 
                    WHERE userId = "+ Id.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);

        return user;
    }

     [HttpPost("adduser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"INSERT INTO BookAppSchema.Users(
           [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
        ) VALUES (" +
                "'" + user.FirstName +
                "', '" + user.LastName +
                "', '" + user.Email +
                "', '" + user.Gender +
                "', '" + user.Active +
        "')";
        if(_dapper.Execute(sql))
            {
                return Ok();
            }
        throw new Exception("Failed to add user");
    }

    [HttpPut("edituser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE BookAppSchema.Users
                SET   
                    [FirstName] = '" + user.FirstName +
                    "', [LastName] = '" + user.LastName +
                    "', [Email] = '" + user.Email +
                    "', [Gender] = '" + user.Gender +
                    "', [Active] = '" + user.Active +
                "'WHERE [userId] = " + user.UserId.ToString();
            if(_dapper.Execute(sql))
            {
                return Ok();
            }
        throw new Exception("Failed to update user");
    }

    [HttpDelete("User/{Id}")]
    public IActionResult DeleteUser(int Id)
    {
        string sql = @"
            DELETE FROM BookAppSchema.Users
                    WHERE userId = "+ Id.ToString();

        if(_dapper.Execute(sql))
            {
                return Ok();
            }
        throw new Exception("Failed to Delete user");
    }

}
