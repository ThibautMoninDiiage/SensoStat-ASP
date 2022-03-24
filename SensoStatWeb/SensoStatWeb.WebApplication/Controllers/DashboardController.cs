using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.WebApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensoStatWeb.WebApplication.Controllers
{
    public class DashboardController : Controller
    {
        #region privates
        private readonly ISurveyService _surveyService;
        private readonly IUserService _userService;
        #endregion

        #region CTOR
        public DashboardController(ISurveyService surveyService, IUserService userService)
        {
            _surveyService = surveyService;
            _userService = userService;
        }
        #endregion

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            // Call the GetSurveys() method in surveyService
            var surveys = await _surveyService.GetSurveys(HttpContext.Request.Cookies["Token"]);

            var model = new DashboardViewModel
            {
                CountTotalSurveys = surveys.Count(),
                CountDeployedSurveys = surveys.Where(survey => survey.StateId == 2).Count(),
                CountUndeployedSurveys = surveys.Where(survey => survey.StateId == 1).Count(),
                FrameworkVersion = typeof(string).Assembly.ImageRuntimeVersion,
                WebsiteVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            };

            return View("Index", model);
        }
    }
}

