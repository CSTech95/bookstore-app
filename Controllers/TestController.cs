using BookStoreApp.Data;
using BookStoreApp.Dtos;
using BookStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    DataContextDapper _dapper;
    public TestController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("Connection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet]
    public string Test()
    {
        return "Test Route Reached";
    }
    

}
