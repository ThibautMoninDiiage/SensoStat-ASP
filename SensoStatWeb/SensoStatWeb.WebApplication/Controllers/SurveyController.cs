using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;
using System.Linq;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IFileService _fileService;

        public SurveyController(ISurveyService surveyService, IFileService fileService)
        {
            _surveyService = surveyService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Detail(string surveyName, IFormFile file)
        {
            var content = await _fileService.ReadCsvFile(file.OpenReadStream());
            var finalResult = content.Split("\r\n").Select(l => l.Split(";"));

            var model = new SurveyViewModel
            {
                Survey = new Survey { Name = surveyName},
                PresentationPlan = finalResult
            };

            return this.View("Detail", model);
        }

        [HttpPost]
        public IActionResult CreateSurvey(List<string>? inputQuestionInstruction, string? orderInputs, string surveyName)
        {
            var listPosition = orderInputs?.Substring(1)?.Split(" ").ToList();

            var questions = new List<Question>();
            var instructions = new List<Instruction>();

            for (int i = 1; i <= inputQuestionInstruction?.Count(); i++)
            {
                if (listPosition?[i - 1] == "question")
                {
                    var question = new Question()
                    {
                        Libelle = inputQuestionInstruction[i - 1],
                        Position = i
                    };

                    questions.Add(question);
                }
                else
                {
                    var instruction = new Instruction()
                    {
                        Libelle = inputQuestionInstruction[i - 1],
                        Position = i
                    };
                    instructions.Add(instruction);
                }
            }
            var survey = new Survey
            {
                Name = surveyName,
                Questions = questions,
                CreationDate = DateTime.Now,
            };

            var resultCreation = _surveyService.CreateSurvey(survey);

            if (resultCreation == null)
                return this.View("Detail");

            return RedirectToAction("index", "surveys");
        }
    }
}

