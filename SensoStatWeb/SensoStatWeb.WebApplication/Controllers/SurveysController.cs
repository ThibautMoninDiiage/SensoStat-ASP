using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveysController : Controller
    {
        public IActionResult Index()
        {
            var survey = new Survey();

            var model = new SurveysViewModel
            {
                Surveys = new List<Survey> { survey, survey}
            };

            return this.View(model);
        }
    }
}

