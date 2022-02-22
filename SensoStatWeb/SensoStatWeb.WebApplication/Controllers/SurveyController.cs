using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;
using System.Linq;

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
        public IActionResult CreateSurvey(List<string> inputQuestionInstruction, string orderInputs)
        {
            orderInputs = orderInputs.Substring(1);
            var listPosition = orderInputs.Split(" ").ToList();

            for (int i = 0; i < listPosition.Count(); i++)
            {
                  
            }

            return RedirectToAction("index", "surveys");
        }
    }
}

