﻿using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.WebApplication.ViewModels;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public async Task<IActionResult> Index()

        {
            var surveys = await _surveyService.GetSurveys(HttpContext.Request.Cookies["Token"]);

            var model = new SurveysViewModel
            {
                Surveys = surveys
            };

            return this.View("index", model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            var deletedSurvey = await _surveyService.DeleteSurvey(id, HttpContext.Request.Cookies["Token"]);

            return RedirectToAction("index", "login");
        }
    }
}

