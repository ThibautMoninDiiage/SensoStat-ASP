using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IAnswerService
    {
        /// <summary>
        /// Call GetSurveyAnswers() method in API (GET)
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId, string token);
    }
}
