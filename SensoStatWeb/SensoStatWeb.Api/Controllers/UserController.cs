using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class UserController : Controller
{
    #region privates
    private readonly IUserServices _userServices;
    #endregion

    #region CTOR
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    #endregion

    #region methods

    #region UsersUrls
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> User([FromQuery] int id)
    {
        var usersUrls = await _userServices.CreateUrl(id);

        return usersUrls == null ? NotFound() : Ok(usersUrls);
    }
    #endregion

    #endregion
}

