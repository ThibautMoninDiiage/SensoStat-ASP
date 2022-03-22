using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;
using System.Linq;
using SensoStatWeb.WebApplication.Services.Interfaces;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities.Interfaces;
using SensoStatWeb.WebApplication.Wrappers;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IAnswerService _answerService;
        private SurveyCreationDTODown _surveyCreationDTODown = new SurveyCreationDTODown();
        private IEnumerable<Product> _products;

        public SurveyController(ISurveyService surveyService, IUserService userService, IFileService fileService, IAnswerService answerService)
        {
            _surveyService = surveyService;
            _fileService = fileService;
            _userService = userService;
            _answerService = answerService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(string surveyName, IFormFile file)
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

            await _surveyService.CreateSurvey(_surveyCreationDTODown,HttpContext.Request.Cookies["Token"]);


            return RedirectToAction("index", "surveys");
        }

        public async Task<IActionResult> EditPage(int surveyId = 0)
        {
            try
            {
                if (surveyId == 0)
                    new Exception("Survey Id can't be 0");

                var survey = await _surveyService.GetSurvey(surveyId, HttpContext.Request.Cookies["Token"]);

                if (survey != null)
                {
                    var questionInstructions = new List<QuestionInstructionWrapper>();

                    questionInstructions.AddRange(survey.Instructions?.Select(x => new QuestionInstructionWrapper(x)));
                    questionInstructions.AddRange(survey.Questions?.Select(x => new QuestionInstructionWrapper(x)));

                    questionInstructions.OrderBy(y => y.Position);


                    var model = new SurveyViewModel
                    {
                        Survey = survey,
                        QuestionsInstructions = questionInstructions.OrderBy(x => x.Position)
                    };


                    return View("detail", model);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("index", "surveys");
        }
        
        public async Task<IActionResult> EditSurvey(Survey survey, List<string>? inputQuestionInstruction, List<string> inputListPosition)
        {
            var questions = new List<Question>();
            var instructions = new List<Instruction>();

            for (int i = 0; i <= inputQuestionInstruction?.Count()-1; i++)
            {
                if (inputListPosition?[i].ToLower() == "question")
                {
                    var question = new Question()
                    {
                        Libelle = inputQuestionInstruction[i],
                        Position = i
                    };

                    questions.Add(question);
                }
                else
                {
                    var instructionStatusCode = 1; // Set status to default

                    switch (inputListPosition?[i].ToLower())
                    {
                        case "introduction":
                            instructionStatusCode = 0;
                            break;
                        case "conclusion":
                            instructionStatusCode = 2;
                            break ;;
                    }

                    var instruction = new Instruction()
                    {
                        Libelle = inputQuestionInstruction[i],
                        Position = i,
                        Status = instructionStatusCode
                    };
                    instructions.Add(instruction);
                }
            }

            var baseSurvey = await _surveyService.GetSurvey(survey.Id, HttpContext.Request.Cookies["Token"]);

            var surveyToUpdate = new Survey()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = new List<Question>(),
                Instructions = new List<Instruction>()
            };


            foreach (var question in questions)
            {
                var baseQuestion = baseSurvey.Questions?.FirstOrDefault(q => q.Libelle == question.Libelle);

                if (baseQuestion != null)
                {
                    baseQuestion.Position = question.Position;
                    surveyToUpdate.Questions?.Add(baseQuestion);
                }
                else
                {
                    surveyToUpdate.Questions?.Add(question);
                }
            }

            foreach (var instruction in instructions)
            {
                var baseInstruction = baseSurvey.Instructions?.FirstOrDefault(i => i.Libelle == instruction.Libelle);

                if (baseInstruction != null)
                {
                    baseInstruction.Position = instruction.Position;
                    surveyToUpdate.Instructions?.Add(baseInstruction);
                }
                else
                {
                    surveyToUpdate.Instructions?.Add(instruction);
                }
            }

            var resultCreation = await _surveyService.UpdateSurvey(surveyToUpdate, HttpContext.Request.Cookies["Token"]);

            return RedirectToAction("index", "surveys");
        }

        public async Task<FileResult> DownloadUsersUrls(int surveyId)
        {
            var usersUrls = await _userService.GetUsersUrls(surveyId, HttpContext.Request.Cookies["Token"]);

            var csvFileStream = await _fileService.WriteCsvFile(usersUrls);

            return new FileStreamResult(csvFileStream, "text/plain")
            {
                FileDownloadName = "liens utilisateurs.csv"
            };
        }

        public async Task<FileResult> DownloadUsersAnswers(int surveyId)
        {
            var answers = await _answerService.GetSurveyAnswers(surveyId, HttpContext.Request.Cookies["Token"]);

            var csvFileStream = await _fileService.WriteCsvFile(answers);

            return new FileStreamResult(csvFileStream, "text/plain")
            {
                FileDownloadName = "réponses utilisateurs.csv"
            };
        }

        public async Task<IActionResult> Deploy(int surveyId,string action)
        {
            var deploySurvey = await _surveyService.Deploy(surveyId, action, HttpContext.Request.Cookies["Token"]);
            return await EditPage(surveyId);
        }

        public async Task<IActionResult> Undeploy(int surveyId, string action)
        {
            var deploySurvey = await _surveyService.Undeploy(surveyId, action, HttpContext.Request.Cookies["Token"]);
            return await EditPage(surveyId);
        }
    }
}