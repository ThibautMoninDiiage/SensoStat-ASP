﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllSurveys()
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
                Status = "200",
                Surveys = result
            });
        }
    }
}
