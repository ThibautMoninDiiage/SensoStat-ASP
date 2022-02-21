using System;
using SensoStatWeb.Business.Interfaces;
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

        public Task<List<Survey>> GetSurveys()
        {
            var surveys = await _httpService.SendHttpRequest<List<Survey>>($"{Constants.BaseUrlApi}/Survey", HttpMethod.Get);

            throw new NotImplementedException();
        }

        public Task<bool> DeleteSurvey()
        {
            throw new NotImplementedException();
        }

        public Task<Survey> GetSurvey()
        {
            throw new NotImplementedException();
        }

        public Task<Survey> UpdateSurvey()
        {
            throw new NotImplementedException();
        }
    }
}

