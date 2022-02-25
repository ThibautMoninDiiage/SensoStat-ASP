using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<List<Survey>> GetSurveys();

        Task<Survey> GetSurvey();

        Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown);

        Task<bool> DeleteSurvey();

        Task<Survey> UpdateSurvey();

    }
}