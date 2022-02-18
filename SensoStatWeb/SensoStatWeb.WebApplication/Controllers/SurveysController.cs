using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveysController : Controller
    {
        public IActionResult Index()
        {
            var model = new SurveysViewModel
            {

            };

            return this.View(model);
        }
    }
}

