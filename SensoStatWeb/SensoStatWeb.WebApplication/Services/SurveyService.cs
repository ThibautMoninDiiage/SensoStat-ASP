using System;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class SurveyService : ISurveyService
    {
        #region privates
        private readonly IHttpService _httpService;
        #endregion

        #region CTOR
        public SurveyService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        #endregion

        #region GetSurveys()
        public async Task<List<Survey>> GetSurveys(string token = "")
        {
            // Get route + endpoint
            var surveys = await _httpService.SendHttpRequest<List<Survey>>($"{Constants.BaseUrlApi}survey", HttpMethod.Get,bearer:token);

            // Return the surveys
            return surveys;
        }
        #endregion

        #region GetSurvey()
        public async Task<Survey> GetSurvey(int surveyId, string token = "")
        {
            // Get route + endpoint
            var survey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?surveyId={surveyId}", HttpMethod.Get, bearer:token);

            // Return a survey
            return survey;
        }
        #endregion

        #region UpdateSurvey()
        public async Task<Survey> UpdateSurvey(Survey survey, string token = "")
        {
            // Get route + endpoint
            var surveyUpdated = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey", HttpMethod.Put, survey, token);

            // Return the updated survey
            return surveyUpdated;
        }
        #endregion

        #region CreateSurvey
        public async Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown, string token = "")
        {
            // Get route + endpoint
            var survey = await _httpService.SendHttpRequest<SurveyCreationDTODown>($"{Constants.BaseUrlApi}survey", HttpMethod.Post, surveyCreationDTODown,token);
            // Return the created survey
            return survey;
        }
        #endregion

        #region DeleteSurvey()
        public async Task<bool> DeleteSurvey(int id, string token = "")
        {
            // Get route + endpoint
            var deletedSurvey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?id={id}", HttpMethod.Delete, bearer:token);
            // Verify if the survey has been deleted
            return deletedSurvey == null;
        }
        #endregion

        #region Deploy()
        public async Task<bool> Deploy(int surveyId, string action, string token = "")
        {
            // Get route + endpoint
            var deploySurvey = await _httpService.SendHttpRequest<bool>($"{Constants.BaseUrlApi}survey/SurveyId?surveyId={surveyId}&action={action}", HttpMethod.Get, bearer: token);
            // Return the deployed survey
            return deploySurvey;
        }
        #endregion

        #region Undeploy
        public async Task<bool> Undeploy(int surveyId, string action, string token = "")
        {
            // Get route + endpoint
            var deploySurvey = await _httpService.SendHttpRequest<bool>($"{Constants.BaseUrlApi}survey/SurveyId?surveyId={surveyId}&action={action}", HttpMethod.Get, bearer: token);
            // Return the survey undeployed
            return deploySurvey;
        }
        #endregion
    }
}
