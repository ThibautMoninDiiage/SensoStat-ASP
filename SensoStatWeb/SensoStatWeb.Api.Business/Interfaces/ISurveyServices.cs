using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface ISurveyServices
    {
        Task<List<Survey>> GetAllSurveys();

        Task<Survey> GetSurvey(int id);

        Task<Survey> GetSurvey(string token);

        Task<Survey> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown);

        Task<Survey> UpdateSurvey(Survey survey);

        Task<bool> DeleteSurvey(int id);

        Task<bool> DeploySurvey(int surveyId);

        Task<bool> UndeploySurvey(int surveyId);
    }
}
