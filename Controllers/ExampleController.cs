using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CSharpExample.Controllers;

[ApiController]
[Route("/")]
public class ExampleController : Controller
{

    private ApiContext _db;

    public ExampleController(ApiContext db)
    {
        _db = db;
    }

    [HttpGet("Ping")]
    public IActionResult Ping()
    {
        const string Body = "Hello World!";
        _db.Add(new ApiCall() { Body = Body, TimeSent = DateTime.UtcNow });
        _db.SaveChanges();

        return Ok(Body);
    }

    [HttpGet("Messages")]
    public IActionResult GetMessages()
    {
        return Ok(_db.Calls);
    }

    [HttpDelete("Messages")]
    public IActionResult DeleteMessage(int id)
    {
        ApiCall blog = _db.Calls.Single(x => x.Id == id);

        if(blog == null)
            return StatusCode(418);

        _db.Remove(blog);
        _db.SaveChanges();
        return Ok();
    }
}