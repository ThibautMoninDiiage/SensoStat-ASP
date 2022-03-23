using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class AnswerService : IAnswerService
    {
        #region privates
        private readonly IHttpService _httpService;
        #endregion

        #region CTOR
        public AnswerService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        #endregion

        #region GetSurveyAnswers()
        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId, string token)
        {
            // Get route + endpoint
            var answers = await _httpService.SendHttpRequest<IEnumerable<SurveyAnswersDTODown>>($"{Constants.BaseUrlApi}answer?surveyId={surveyId}", HttpMethod.Get, bearer:token);

            // return the answers of the survey
            return answers;
        }
        #endregion
    }
}
