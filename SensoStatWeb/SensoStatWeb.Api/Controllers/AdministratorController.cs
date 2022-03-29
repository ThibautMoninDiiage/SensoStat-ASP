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
        #region privates
        private readonly IAdministratorServices _administratorServices;
        #endregion

        #region CTOR
        public AdministratorController(IAdministratorServices administratorServices)
        {
            _administratorServices = administratorServices;
        }
        #endregion

        #region methods

        #region Login
        /// <summary>
        /// Route for login (ex : https://localhost:7019/Administrator?login=plop&password=plop )
        /// </summary>
        /// <param name="login">A user login (ex : plop)</param>
        /// <param name="password">A user password ( ex : plop)</param>
        /// <returns>An AdministratorTokenDTODown compose by a administrator and a token</returns>
        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] string login, [FromQuery] string password)
        {
            // If the login or the password is null we return an error
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return BadRequest();

            // Get the administrator in the database
            var admin = await _administratorServices.Login(login, password);

            // If the admin are found we return all infos else we return not found
            return admin == null ? NotFound() : Ok(
                new AdministratorTokenDTODown
                {
                    Administrator = admin.Administrator,
                    Token = admin.Token,
                }
            );
        }
        #endregion

        #region Register
        /// <summary>
        /// Route for register (ex : https://localhost:7019/Administrator ) with an AdministratorDTOUp in body
        /// </summary>
        /// <param name="administrator">An AdministratorDTOUp in body</param>
        /// <returns>Ok if the admin is created else NotFound</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] AdministratorDTOUp administrator)
        {
            var resultRegister = await _administratorServices.Register(administrator);

            if (resultRegister)
                return Ok();

            return NotFound();
        }
        #endregion

        #endregion
    }
}

