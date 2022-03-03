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
        private readonly IHttpService _httpService;

        public SurveyService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Survey>> GetSurveys()
        {
            var surveys = await _httpService.SendHttpRequest<List<Survey>>($"{Constants.BaseUrlApi}survey", HttpMethod.Get);

            return surveys;
        }



        public Task<bool> DeleteSurvey()
        {
            throw new NotImplementedException();
        }

        public async Task<Survey> GetSurvey(int surveyId)
        {
            var survey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?surveyId={surveyId}", HttpMethod.Get);

            return survey;
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            var surveyUpdated = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey", HttpMethod.Put, survey);

            return surveyUpdated;
        }

        public async Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown)
        {
            var result = await _httpService.SendHttpRequest<SurveyCreationDTODown>($"{Constants.BaseUrlApi}survey", HttpMethod.Post, surveyCreationDTODown);
            return result;
        }
    }
}
