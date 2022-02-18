using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveysController : Controller
    {
        private readonly IHttpService _httpService;

        public SurveysController(IHttpService httpService)
        {
            _httpService = httpService;
        }

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

