using Microsoft.AspNetCore.Mvc;
using SensoStatApi.Models;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class LoginController : Controller
{
    private SensoStatDbContext? _context;

    public LoginController(SensoStatDbContext? context)
    {
        this._context = context;
    }

    // GET: LoginController
    [HttpGet]
    public IActionResult Get([FromQuery] string login, [FromQuery] string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return BadRequest();
        var admin = _context.Administrators.FirstOrDefault(a => a.UserName == login && a.Password == password);
        if (admin == null) return NotFound();

        return Ok(new
        {
            Status = "200"
        });
    }
}
