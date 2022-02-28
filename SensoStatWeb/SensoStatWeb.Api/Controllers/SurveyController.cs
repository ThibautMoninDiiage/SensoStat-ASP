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
    private readonly IUserRepository _userRepository;
    private readonly ISurveyStateRepository _surveyStateRepository;

    public SurveyController(ISurveyRepository surveyRepository, IAdministratorRepository administratorRepository,IUserRepository userRepository,ISurveyStateRepository surveyStateRepository)
    {
        _surveyRepository = surveyRepository;
        _administratorRepository = administratorRepository;
        _userRepository = userRepository;
        _surveyStateRepository = surveyStateRepository;
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
    public async Task<IActionResult> Survey([FromBody]SurveyCreationDTODown surveyCreationDTODown)
    {
        //Survey survey = new Survey()
        //{
        //    Name = surveyCreationDTODown.Name,
        //    Instructions = surveyCreationDTODown.Instructions,
        //    Questions = surveyCreationDTODown.Questions,
        //    UserProducts = surveyCreationDTODown.UserProducts,
        //    Administrator = _administratorRepository.GetAdministrator(surveyCreationDTODown.AdminId),
        //    CreationDate = surveyCreationDTODown.CreationDate,
        //    CreatorId = surveyCreationDTODown.AdminId,
        //    Id = _surveyRepository.GetAllSurveys().Count + 1,
        //    StateId = 1,
        //    SurveyState = _surveyStateRepository.GetSurveyState(1),
        //    User = _userRepository.GetUser(1),
        //    UserId = 1,
        //};

        var survey = new Survey()
        {
            Name = surveyCreationDTODown.Name,
            CreatorId = 1,
            Administrator = _administratorRepository.GetAdministrator(1),
            UserProducts = new List<UserProduct>(),
            CreationDate = DateTime.Now,
            SurveyState = _surveyStateRepository.GetSurveyState(1),
            StateId = 1,
            Users = new List<User>(),
            Instructions = new List<Instruction>(),
            Questions = new List<Question>(),
        };

        var result = await _surveyRepository.CreateSurvey(survey);


        return result == null ? NotFound() : Ok(result);
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
