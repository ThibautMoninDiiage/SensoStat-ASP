using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAdministratorService _administratorService;

        public RegisterController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        public IActionResult Index()
        {
            return this.View("index");
        }

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var admin = new AdministratorDTOUp()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            };

            _administratorService.Register(admin, HttpContext.Request.Cookies["Token"]);
            return RedirectToAction("index", "surveys");
        }
    }
}

