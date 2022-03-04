using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    public IActionResult User([FromQuery]int id)
    {
        var result = _userRepository.CreateUrl(id);
        return Ok(result);
    }
}

