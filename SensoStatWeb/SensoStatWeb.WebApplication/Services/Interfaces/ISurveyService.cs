using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<List<Survey>> GetSurveys(string token);

        Task<Survey> GetSurvey(string token);

        Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown,string token);

        Task<bool> DeleteSurvey(string token);

        Task<Survey> UpdateSurvey(string token);

    }
}