using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.Commons;
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

        public async Task<IActionResult> Index()
        {
            var surveys = await _httpService.SendHttpRequest<List<Survey>>($"{Constants.BaseUrlApi}/Survey", HttpMethod.Get) ;

            var model = new SurveysViewModel
            {
                Surveys = surveys
            };

            return this.View(model);
        }

        public IActionResult CreateSurvey()
        {
            return View("Index");
        }
    }
}

