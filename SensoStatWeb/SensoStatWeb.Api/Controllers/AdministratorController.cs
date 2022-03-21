using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;

namespace SensoStatWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorServices _administratorServices;

        public AdministratorController(IAdministratorServices administratorServices)
        {
            _administratorServices = administratorServices;
        }


        // Route for login
        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] string login, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return BadRequest();
            var admin = await _administratorServices.Login(login, password);
            return admin == null ? NotFound() : Ok(
                new AdministratorTokenDTODown
                {
                    Administrator = admin.Administrator,
                    Token = admin.Token,
                }
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] AdministratorDTOUp administrator)
        {
            var resultRegister = await _administratorServices.Register(administrator);

            if (resultRegister)
                return Ok();

            return NotFound();
        }
    }
}

