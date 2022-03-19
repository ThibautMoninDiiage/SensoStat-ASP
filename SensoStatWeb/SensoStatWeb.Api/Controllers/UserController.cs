using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class UserController : Controller
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> User([FromQuery] int id)
    {
        var usersUrls = await _userServices.CreateUrl(id);

        return Ok(usersUrls);
    }
}

