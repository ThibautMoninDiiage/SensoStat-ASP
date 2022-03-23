using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Up;

namespace SensoStatWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : Controller
    {
        #region privates
        private readonly IAnswerService _answerService;
        #endregion

        #region CTOR
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        #endregion

        #region methods

        #region GetAnswers
        /// <summary>
        /// This method return all answers of a answer
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>All answers (SurveyAnswerDtoDown) of one survey</returns>
        [HttpGet]
        [Authorize]
        [ActionName("Answer")]
        public async Task<IActionResult> GetAnswers(int surveyId)
        {
            // Get all answers of our survey
            var answers = await _answerService.GetSurveyAnswers(surveyId);

            // if our survey exist return all answers, else return NotFound()
            return answers == null ? NotFound() : Ok(answers);
        }
        #endregion

        #region CreateAnswer
        /// <summary>
        /// Create an answer in database
        /// </summary>
        /// <param name="answerDTOUp">The answer in AnswerDTOUp</param>
        /// <returns>200 if answer created else 404</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerDTOUp answerDTOUp)
        {
            var postAnswer = await _answerService.CreateAnswer(answerDTOUp);

            return postAnswer == null ? NotFound() : Ok();
        }
        #endregion

        #endregion
    }
}
