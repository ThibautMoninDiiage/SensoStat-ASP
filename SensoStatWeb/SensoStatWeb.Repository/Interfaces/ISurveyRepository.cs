using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        List<Survey> GetAllSurveys();

        Task<Survey> CreateSurvey(Survey survey);

        Task<Survey> UpdateSurvey(Survey survey);

        Task<bool> DeleteSurvey(int id);
    }
}
