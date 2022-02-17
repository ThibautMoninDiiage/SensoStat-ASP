using System;
using Microsoft.AspNetCore.Mvc;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveysController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

