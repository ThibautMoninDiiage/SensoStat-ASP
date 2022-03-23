using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<List<SurveyWithStatsDtoDown>> GetSurveys(string token = "");

        Task<Survey> GetSurvey(int surveyId, string token = "");

        Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown, string token = "");

        Task<bool> DeleteSurvey(int surveyId, string token = "");

        Task<Survey> UpdateSurvey(Survey survey, string token = "");

        Task<bool> Deploy(int surveyId, string action ,string token = "");

        Task<bool> Undeploy(int surveyId, string action, string token = "");
    }
}