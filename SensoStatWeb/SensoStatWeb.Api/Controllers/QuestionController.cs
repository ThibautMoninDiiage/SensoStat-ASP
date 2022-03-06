using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class QuestionController : Controller
{
    private readonly IQuestionServices _questionServices;
    public QuestionController(IQuestionServices questionServices)
    {
        _questionServices = questionServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Questions()
    {
        var result =await _questionServices.GetAllQuestions();
        return result == null ?  NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Question([FromBody]Question question)
    {
        var result =await _questionServices.CreateQuestion(question);
        return result == null ? NotFound() : Ok(question);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Question([FromBody]int id)
    {
        var result =await _questionServices.DeleteQuestion(id);
        return result == null ? NotFound() : Ok(result);
    }
}

