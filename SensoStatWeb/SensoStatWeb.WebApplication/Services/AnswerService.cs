using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class AnswerService : IAnswerService
    {
        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
