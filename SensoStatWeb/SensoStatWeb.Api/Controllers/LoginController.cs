using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class LoginController : Controller
{
    public IAdministratorServices _administratorServices;

    public LoginController(IAdministratorServices administratorServices)
    {
        _administratorServices = administratorServices;
    }

    // GET: LoginController
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string login, [FromQuery] string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return BadRequest();
        var admin =await _administratorServices.Login(login,password);
        return admin == null ? NotFound() : Ok(
            new AdministratorTokenDTODown
            {
                Administrator = admin.Administrator,
                Token = admin.Token,
            }
        );
    }
}
