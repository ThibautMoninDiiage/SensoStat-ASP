using System;
using Microsoft.AspNetCore.Mvc;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Detail()
        {
            return this.View("Detail");
        }
    }
}

