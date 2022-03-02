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
            _surveyCreationDTODown.Name = surveyName;

            var content = await _fileService.ReadCsvFile(file.OpenReadStream());
            var finalResult = content.Split("\r\n").Select(l => l.Replace("\"", "")).Select(l => l.Split(";"));

            var presentationPlan = finalResult.Skip(1).Take(finalResult.Count() - 2).Select(o => new PresentationPlanDTODown() { UserCode = o[1], Products = o.Skip(2)}).ToList();

            // Create each product + Distinct
            _products = presentationPlan
                .SelectMany(x => x.Products)
                .Distinct()
                .Select(x => new Product() { Code = x });

            _surveyCreationDTODown.Users = new List<User>();

            presentationPlan.ForEach(p =>_surveyCreationDTODown.Users.Add(new User() { Code = p.UserCode }));

            _surveyCreationDTODown.Products = new List<Product>();

            _surveyCreationDTODown.Products = _products.ToList();

            _surveyCreationDTODown.UserProducts = new List<UserProduct>();

            foreach (var surveyUser in presentationPlan)
            {
                for (int i = 0; i < surveyUser.Products.Count(); i++)
                {
                    var userProduct = new UserProduct()
                    {
                        User = new User() { Code = surveyUser.UserCode },
                        Position = i,
                        Product = _products.FirstOrDefault(x => x.Code == surveyUser.Products.ToList()[i]),
                    };

                    _surveyCreationDTODown.UserProducts.Add(userProduct);

                }
            }

            await _surveyService.CreateSurvey(_surveyCreationDTODown);


            return RedirectToAction("index", "surveys");
        }

        

        public async Task<IActionResult> CreateSurvey(SurveyCreationDTODown SurveyCreationDTODown, List<string>? inputQuestionInstruction, string? orderInputs)
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
            //var survey = new SurveyCreationDTODown
            //{
            //    Id = 1,
            //    Name = SurveyCreationDTODown.Name,
            //    Questions = questions,
            //    Instructions = instructions,
            //    AdminId = 1,
            //    CreationDate = DateTime.Now,
            //    UserProducts = userProducts
            //};

            // var resultCreation = await _surveyService.CreateSurvey(survey);


            if (false)
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

