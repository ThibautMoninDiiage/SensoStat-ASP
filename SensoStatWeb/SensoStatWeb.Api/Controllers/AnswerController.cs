using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;

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
        public async Task<IActionResult> Answers(int surveyId)
        {
            var answers = await _answerService.GetSurveyAnswers(surveyId);

            return Ok(answers);
        }
    }
}
