using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SurveyController : Controller
{
    private readonly ISurveyRepository _surveyRepository;
    public SurveyController(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    [HttpGet]
    // GET: SurveyController
    public IActionResult Survey()
    {
        var result = _surveyRepository.GetAllSurveys();
        if(result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(new
            {
                Surveys = result
            });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody]Survey survey)
    {
        _surveyRepository.CreateSurvey(survey);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody]Survey survey)
    {
        _surveyRepository.UpdateSurvey(survey);
        return Ok();
    }
}
