using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.WebApplication.ViewModels;

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

        [HttpPost]
        public IActionResult CreateSurveyPost(CreateSurveyViewModel surveyViewModel)
        {
            if (this.ModelState.IsValid)
            {
                return this.View("Detail");
            }

            return this.View("Index");
        }
    }
}

