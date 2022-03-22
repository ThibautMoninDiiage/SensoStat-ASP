using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        [Authorize]
        [ActionName("Answer")]
        public async Task<IActionResult> GetAnswers(int surveyId)
        {
            var answers = await _answerService.GetSurveyAnswers(surveyId);

            return Ok(answers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody]AnswerDTOUp answerDTOUp)
        {
            var postAnswer = await _answerService.CreateAnswer(answerDTOUp);

            return postAnswer == null ? NotFound() : Ok(postAnswer);
        }
    }
}
