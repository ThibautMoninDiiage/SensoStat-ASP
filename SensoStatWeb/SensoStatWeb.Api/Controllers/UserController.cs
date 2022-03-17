using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Repository.Interfaces;
using SensoStatWeb.Api.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

