using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;
using System.Linq;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SurveyController : Controller
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IAdministratorRepository _administratorRepository;

    public SurveyController(ISurveyRepository surveyRepository, IAdministratorRepository administratorRepository)
    {
        _surveyRepository = surveyRepository;
        _administratorRepository = administratorRepository;
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
    public IActionResult Survey([FromBody]SurveyCreationDTODown surveyCreationDTODown)
    {
        Survey survey = new Survey()
        {
            Name = surveyCreationDTODown.Name,
            Instructions = surveyCreationDTODown.Instructions,
            Questions = surveyCreationDTODown.Questions,
            Products = surveyCreationDTODown.Products,
            Administrator = _administratorRepository.GetAdministrator(surveyCreationDTODown.AdminId),
            CreationDate = DateTime.Now,
            CreatorId = surveyCreationDTODown.AdminId,
            Id = surveyCreationDTODown.Id,
            StateId = 1,
            SurveyState = _surveyRepository.GetSurvey(surveyCreationDTODown.Id).SurveyState,
            User = surveyCreationDTODown.Users.FirstOrDefault(),
            UserId = 1
        };

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
