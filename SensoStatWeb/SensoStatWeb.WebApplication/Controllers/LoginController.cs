﻿using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Business.Interfaces;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpService _httpService;

        public LoginController(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            return this.View("../Surveys/Index", loginViewModel); // redirect to Index page


            // If the model state are not valid
            if (!ModelState.IsValid)
                return this.View("Index", loginViewModel); // redirect to Index page

            var url = $"https://localhost:7270/login?login={loginViewModel.Username}&mdp={loginViewModel.Password}";
            var resultLogin = await _httpService.SendHttpRequest<HttpResultDTODown>(url, HttpMethod.Get);

            // If the password or the username are false
            if (resultLogin == null)
            {
                loginViewModel.ErrorMessage = "Nom d'utilisateur ou mot de passe incorect";
                return this.View("Index", loginViewModel);
            }

            // When the password is correct
            return this.View("Index");
        }
    }
}
}

