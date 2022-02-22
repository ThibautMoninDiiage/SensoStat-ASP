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
            return Ok(result);
        }
    }

    [HttpPost]
    public IActionResult Survey([FromBody]Survey survey)
    {
        var result = _surveyRepository.CreateSurvey(survey);
        if(result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody]Survey survey)
    {
        var result = _surveyRepository.UpdateSurvey(survey);
        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpDelete]
    public IActionResult Survey(int id)
    {
        var result = _surveyRepository.DeleteSurvey(id);
        if(result.Result != true)
        {
            return Ok(result.Result);
        }
        else
        {
            return NotFound();
        }
    }


}
