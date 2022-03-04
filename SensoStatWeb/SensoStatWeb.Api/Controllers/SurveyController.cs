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
    public async Task<IActionResult> Survey(int surveyId = 0)
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Survey([FromBody]SurveyCreationDTODown surveyCreationDTODown)
    {

        var createdSurvey = await _surveyServices.CreateSurvey(surveyCreationDTODown);

        var createdUsers = await _userServices.CreateUsers(surveyCreationDTODown.Users,createdSurvey);

        var createdProducts = await _productServices.CreateProducts(surveyCreationDTODown.Products,createdSurvey);

        var createdUserProduct = await _userProductServices.CreateUserProducts(surveyCreationDTODown.UserProducts,createdSurvey,createdUsers,createdProducts);

        return createdSurvey == null ? NotFound() : Ok(createdSurvey);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody]Survey survey)
    {
        var result = await _surveyServices.UpdateSurvey(survey);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> SurveyDelete(int id)
    {
        var result = await _surveyServices.DeleteSurvey(id);
        return result != true ? Ok(result) : NotFound();
    }

    [HttpGet("UserId")]
    [ActionName("Survey")]
    [Authorize]
    public async Task<IActionResult> GetSurveyByUserId([FromRoute] int userId)
    {
        var result =await _surveyServices.GetSurvey(userId);
        return result == null ? NotFound() : Ok(result);
    }

}
