using System;
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
        #region privates
        private readonly ISurveyService _surveyService;
        #endregion

        #region CTOR
        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        #endregion

        #region Index()
        /// <summary>
        /// Return a page
        /// </summary>
        /// <returns>Return the surveys page</returns>
        public async Task<IActionResult> Index()
        {
            // Call the GetSurveys() method in surveyService
            var surveys = await _surveyService.GetSurveys(HttpContext.Request.Cookies["Token"]);

            // Call the SurveysViewModel
            var model = new SurveysViewModel
            {
                Surveys = surveys
            };

            // Return the Index method with the model
            return this.View("index", model);
        }
        #endregion

        #region Logout()
        /// <summary>
        /// Logout an Administrator
        /// </summary>
        /// <returns>Return the Login page</returns>
        public async Task<IActionResult> Logout()
        {
            // Delete cookie
            HttpContext.Response.Cookies.Delete("Token");

            // Redirect to login page
            return RedirectToAction("index", "login");
        }
        #endregion

        #region DeleteSurvey()
        /// <summary>
        /// Delete a survey
        /// </summary>
        /// <param name="id">The id of the survey</param>
        /// <returns>Delete a survey</returns>
        [HttpDelete]
        public async Task DeleteSurvey(int id)
        {
            // Call the DeleteSurvey() method in surveyService
            var deletedSurvey = await _surveyService.DeleteSurvey(id, HttpContext.Request.Cookies["Token"]);
        }
        #endregion
    }
}

