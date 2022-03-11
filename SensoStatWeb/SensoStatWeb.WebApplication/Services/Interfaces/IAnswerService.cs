using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId, string token);
    }
}
