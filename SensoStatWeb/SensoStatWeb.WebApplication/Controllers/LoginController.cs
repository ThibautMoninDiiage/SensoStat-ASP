using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        #region privates
        private readonly IAdministratorService _administratorService;
        #endregion

        #region CTOR
        public LoginController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }
        #endregion

        #region methods

        #endregion

        #region Index
        /// <summary>
        /// Return to view LogIn
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return this.View();
        }
        #endregion

        #region Authenticate
        /// <summary>
        /// Authenticate the user 
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Authenticate(LoginViewModel loginViewModel)
        {
            // If the model state are not valid
            if (!ModelState.IsValid)
                return this.View("Index", loginViewModel); // redirect to Index page

            var resultLogin = await _administratorService.Login(loginViewModel.Username, loginViewModel.Password);

            // If the password or the username are false
            if (resultLogin == null)
            {
                // Affect an error message to the ErrorMessage property
                loginViewModel.ErrorMessage = "Nom d'utilisateur ou mot de passe incorect";
                // Return to the Login view
                return this.View("Index", loginViewModel);
            }

            // Add the token to the Cookies
            HttpContext.Response.Cookies.Append("Token", resultLogin.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            // When the password is correct
            return RedirectToAction("Index", "surveys");
        }
        #endregion
    }
}