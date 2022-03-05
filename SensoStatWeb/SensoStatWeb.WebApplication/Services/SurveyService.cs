﻿using System;
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

        public async Task<List<Survey>> GetSurveys(string token = "")
        {
            var surveys = await _httpService.SendHttpRequest<List<Survey>>($"{Constants.BaseUrlApi}survey", HttpMethod.Get,bearer:token);

            return surveys;
        }

        public async Task<Survey> GetSurvey(int surveyId, string token = "")
        {
            var survey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?surveyId={surveyId}", HttpMethod.Get, bearer:token);

            return survey;
        }

        public async Task<Survey> UpdateSurvey(Survey survey, string token = "")
        {
            var surveyUpdated = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey", HttpMethod.Put, survey, token);

            return surveyUpdated;
        }

        public async Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown, string token = "")
        {
            var result = await _httpService.SendHttpRequest<SurveyCreationDTODown>($"{Constants.BaseUrlApi}survey", HttpMethod.Post, surveyCreationDTODown,token);
            return result;
        }

        public async Task<bool> DeleteSurvey(int id, string token = "")
        {
            var deletedSurvey = await _httpService.SendHttpRequest<Survey>($"{Constants.BaseUrlApi}survey?id={id}", HttpMethod.Delete, bearer:token);
            return deletedSurvey == null;
        }
    }
}
