using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Detail(string surveyName)
        {
            var model = new SurveyViewModel
            {
                Survey = new Survey { Name = surveyName}
            };

            return this.View("Detail", model);
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

