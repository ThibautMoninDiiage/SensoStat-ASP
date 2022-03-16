using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Down;
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


    //[HttpGet]
    //public async Task<IActionResult> User([FromQuery]int id)
    //{
    //    var result =await _userRepository.CreateUrl(id);

        

    //    return Ok(result.Select(r => new UserUrlDTODown() { Code = r.Code, Url = r.Link}));
    //}
}

