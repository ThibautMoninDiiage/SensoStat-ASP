using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IHttpService _httpService;
        public AnswerService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId, string token)
        {
            var answers = await _httpService.SendHttpRequest<IEnumerable<SurveyAnswersDTODown>>($"{Constants.BaseUrlApi}answer?surveyId={surveyId}", HttpMethod.Get, bearer:token);

            return answers;
        }
    }
}
