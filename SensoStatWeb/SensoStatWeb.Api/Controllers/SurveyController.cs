using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SurveyController : Controller
{
    private readonly ISurveyServices _surveyServices;
    private readonly IUserServices _userServices;
    private readonly IProductServices _productServices;
    private readonly IUserProductServices _userProductServices;

    public SurveyController(ISurveyServices surveyServices, IUserServices userServices, IProductServices productServices, IUserProductServices userProductServices)
    {
        _surveyServices = surveyServices;
        _userServices = userServices;
        _productServices = productServices;
        _userProductServices = userProductServices;
    }

    [HttpGet]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> GetSurvey([FromQuery] int surveyId = 0)
    {
        try
        {
            return surveyId == 0 ? Ok(await _surveyServices.GetAllSurveys()) : Ok(await _surveyServices.GetSurvey(surveyId));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        return NotFound();
    }

    [HttpGet("Token")]
    [ActionName("Survey")]
    public async Task<IActionResult> GetSurveyByToken([FromQuery] string token)
    {
        var survey = await _surveyServices.GetSurvey(token);
        return survey == null ? NotFound() : Ok(survey);
    }


    [HttpPost]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> CreateSurvey([FromBody]SurveyCreationDTODown surveyCreationDTODown)
    {

        var createdSurvey = await _surveyServices.CreateSurvey(surveyCreationDTODown);

        var createdUsers = await _userServices.CreateUsers(surveyCreationDTODown.Users,createdSurvey);

        var createdProducts = await _productServices.CreateProducts(surveyCreationDTODown.Products,createdSurvey);

        var createdUserProduct = await _userProductServices.CreateUserProducts(surveyCreationDTODown.UserProducts,createdSurvey,createdUsers,createdProducts);

        return createdSurvey == null ? NotFound() : Ok(createdSurvey);
    }

    [HttpPut]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> Update([FromBody] Survey survey)
    {
        var result = await _surveyServices.UpdateSurvey(survey);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> Delete([FromQuery]int id)
    {
        var result = _surveyServices.DeleteSurvey(id);

        return await result == false ? NotFound() : Ok(result);
    }

    [HttpGet("SurveyId")]
    [ActionName("Survey")]
    [Authorize]
    public async Task<IActionResult> DeploySurvey([FromQuery] int surveyId, [FromQuery] string action)
    {
        return action == "Deploy" ? Ok(await _surveyServices.DeploySurvey(surveyId)) : Ok(await _surveyServices.UndeploySurvey(surveyId));
    }
}