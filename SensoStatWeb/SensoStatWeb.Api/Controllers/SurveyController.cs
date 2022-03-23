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
    #region privates
    private readonly ISurveyServices _surveyServices;
    private readonly IAnswerService _answerService;
    #endregion

    #region CTOR
    public SurveyController(ISurveyServices surveyServices, IAnswerService answerService)
    {
        _surveyServices = surveyServices;
        _answerService = answerService;
    }
    #endregion

    #region GetSurvey
    /// <summary>
    /// This method allow administrator to get one or many surveys
    /// </summary>
    /// <param name="surveyId">Specify a surveyId to get one specify survey</param>
    /// <returns>A list of all survey or just one specific survey</returns>
    [HttpGet]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> GetSurvey([FromQuery] int surveyId = 0)
    {
        // If the user wan't all surveys surveyId => 0
        if (surveyId == 0)
        {
            var surveys = await _surveyServices.GetAllSurveys();

            var surveyDtoWithStats = surveys.Select(s =>
            
                new SurveyWithStatsDtoDown()
                {
                    Survey = s,
                    PercentageOfCompletion = _answerService.GetSurveyPercentageAnswers(s.Id).Result
                }
            );

           return surveyDtoWithStats != null ? Ok(surveyDtoWithStats) : NotFound();



        }
        else
        {
            var survey = await _surveyServices.GetSurvey(surveyId);
            return survey != null ? Ok(survey) : NotFound();
        }
    }
    #endregion

    #region methods

    #region GetSurveyByToken
    /// <summary>
    /// Get a survey thanks a user token
    /// </summary>
    /// <param name="token">One user token like : eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IlMwMDE1NCIsInByaW1hcnlzaWQiOiI1NCIsImp0aSI6ImZmMjc4Yzg1LTQ4YzYtNDg1ZC04YWM5LTQ4MmYyNGZjOTFkYiIsIm5iZiI6MTY0NzQzMzgzMSwiZXhwIjoxNjQ4MDM4NjMxLCJpYXQiOjE2NDc0MzM4MzEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTkiLCJhdWQiOiJTZW5zb1N0YXRXZWIuQXBpIn0.OMzwO8E4W-d2abdeHlpjkMvYTA_mRQF9KldZ_ySQDdg</param>
    /// <returns>One survey with status 200 if token exist else return 404</returns>
    [HttpGet("Token")]
    [ActionName("Survey")]
    public async Task<IActionResult> GetSurveyByToken([FromQuery] string token)
    {
        var survey = await _surveyServices.GetSurvey(token);
        return survey == null ? NotFound() : Ok(survey);
    }
    #endregion

    #region CreateSurvey
    /// <summary>
    /// This method allow an administrator to create a survey
    /// </summary>
    /// <param name="surveyCreationDTODown">a survey in format of DTO</param>
    /// <returns>A survey if created else 404</returns>
    [HttpPost]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> CreateSurvey([FromBody] SurveyCreationDTODown surveyCreationDTODown)
    {
        surveyCreationDTODown.AdminId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "id").Value);

        var survey = await _surveyServices.CreateSurvey(surveyCreationDTODown);

        return survey == null ? NotFound() : Ok(survey);
    }
    #endregion

    #region PutSurvey
    /// <summary>
    /// Allow administrator to edit a survey
    /// </summary>
    /// <param name="survey">A survey</param>
    /// <returns>404 in case of error else 200 with survey</returns>
    [HttpPut]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> PutSurvey([FromBody] Survey survey)
    {
        var result = await _surveyServices.UpdateSurvey(survey);

        return result == null ? NotFound() : Ok(result);
    }
    #endregion

    #region DeleteSurvey
    /// <summary>
    /// Allow administrator to delete one survey
    /// </summary>
    /// <param name="id">The id of the survey</param>
    /// <returns>200 or 404</returns>
    [HttpDelete]
    [Authorize]
    [ActionName("Survey")]
    public async Task<IActionResult> DeleteSurvey([FromQuery]int id)
    {
        var surveyDeleted = await _surveyServices.DeleteSurvey(id);

        return surveyDeleted ? Ok() : NotFound();
    }
    #endregion

    #region DeploySurvey
    /// <summary>
    /// This method allow user to change state of one survey
    /// </summary>
    /// <param name="surveyId"The id of the survey></param>
    /// <param name="action">The action state</param>
    /// <returns></returns>
    [HttpGet("SurveyId")]
    [ActionName("Survey")]
    [Authorize]
    public async Task<IActionResult> DeploySurvey([FromQuery] int surveyId, [FromQuery] string action)
    {
        return action == "Deploy" ? Ok(await _surveyServices.DeploySurvey(surveyId)) : Ok(await _surveyServices.UndeploySurvey(surveyId));
    }
    #endregion

    #endregion
}