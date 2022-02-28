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
        private SurveyCreationDTODown _surveyCreationDTODown = new SurveyCreationDTODown();
        private IEnumerable<Product> _products;

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

            var presentationPlan = finalResult.Skip(1).Select(o => new PresentationPlanDTODown() { UserCode = o[0], Products = o.Skip(1)}).ToList();

            _products = presentationPlan
                .SelectMany(x => x.Products)
                .Distinct()
                .Select(x => new Product() { Code = Int32.Parse(x) });

            // _products = x.ForEach(o => o.Products.ToList().Select(u => new Product() { Code = Int32.Parse(u)}));



            var model = new SurveyViewModel
            {
                SurveyCreationDTODown = new SurveyCreationDTODown { Name = surveyName},
                PresentationPlan = finalResult
            };

            return this.View("Detail", model);
        }


        public async Task<IActionResult> CreateSurvey(List<string>? inputQuestionInstruction, string? orderInputs, string surveyName)
        {
            var listPosition = orderInputs?.Substring(1)?.Split(" ").ToList();

            var questions = new List<Question>();
            var instructions = new List<Instruction>();
            var userProducts = new List<UserProduct>();

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
            var survey = new SurveyCreationDTODown
            {
                Id = 1,
                Name = surveyName,
                Questions = questions,
                Instructions = instructions,
                AdminId = 1,
                CreationDate = DateTime.Now,
                UserProducts = userProducts
            };

            var resultCreation = await _surveyService.CreateSurvey(survey);

            if (resultCreation != null)
                return this.View("Detail");

            return RedirectToAction("index", "surveys");
        }

        public async Task<IActionResult> DeleteSurvey(int id)
        {
            var deletedSurvey = await _surveyService.DeleteSurvey(id);

            return RedirectToAction("Index", "surveys");
        }
    }
}

