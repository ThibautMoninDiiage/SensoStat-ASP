using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<List<Survey>> GetSurveys();

        Task<Survey> GetSurvey();

        Task<bool> DeleteSurvey();

        Task<Survey> UpdateSurvey();

    }
}