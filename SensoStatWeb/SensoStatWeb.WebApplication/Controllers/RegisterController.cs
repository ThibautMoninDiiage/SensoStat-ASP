using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class RegisterController : Controller
    {
        #region privates
        private readonly IAdministratorService _administratorService;
        #endregion

        #region CTOR
        public RegisterController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }
        #endregion

        #region methods

        #endregion

        #region Index
        /// <summary>
        /// Return to view Register
        /// </summary>
        /// <returns>View Login</returns>
        public IActionResult Index()
        {
            return this.View("index");
        }
        #endregion

        #region Register
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns>View Surveys</returns>
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // If the ModelState isn't valid
            if (!ModelState.IsValid)
            {
                // Return view Register
                return Index();
            }

            // Create an Administrator with DTO
            var admin = new AdministratorDTOUp()
            {
                // Get data from ViewModel
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            };

            // 
            _administratorService.Register(admin, HttpContext.Request.Cookies["Token"]);

            //
            return RedirectToAction("index", "surveys");
        }
        #endregion
    }
}