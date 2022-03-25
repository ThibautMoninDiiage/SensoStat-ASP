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
        #region privates
        private readonly ISurveyService _surveyService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IAnswerService _answerService;
        private SurveyCreationDTODown _surveyCreationDTODown = new SurveyCreationDTODown();
        private IEnumerable<Product> _products;
        #endregion

        #region CTOR
        public SurveyController(ISurveyService surveyService, IUserService userService, IFileService fileService, IAnswerService answerService)
        {
            _surveyService = surveyService;
            _fileService = fileService;
            _userService = userService;
            _answerService = answerService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Return to Survey view
        /// </summary>
        /// <returns>View Index of the survey controller</returns>
        public IActionResult Index()
        {
            return this.View();
        }
        #endregion

        #region CreateSurvey
        /// <summary>
        /// Create a survey with a name and read a CSV file
        /// </summary>
        /// <param name="surveyName">The name of the survey</param>
        /// <param name="file">The CSV file</param>
        /// <returns>Redirect to the Index() method in Surveys controller</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSurvey(string surveyName, IFormFile file)
        {
            // Get the survey name
            _surveyCreationDTODown.Name = surveyName;

            // Call the ReadCsvFile method from fileService
            // Get the content from this file
            var content = await _fileService.ReadCsvFile(file.OpenReadStream());

            // Dispatch our file in lines and cells
            var finalResult = content.Split("\r\n").Select(l => l.Replace("\"", "")).Select(l => l.Split(";")).Where(l => l.Length > 2);

            // Skip the first line
            // Get the content
            var presentationPlan = finalResult.Skip(1)
                .Select(o => new PresentationPlanDTODown() { UserCode = o[1], Products = o.Skip(2)}).ToList();

            // Create each product + Distinct
            _products = presentationPlan
                .SelectMany(x => x.Products)
                .Distinct()
                .Select(x => new Product() { Code = x });

            // Bind a list of user to the Users property
            _surveyCreationDTODown.Users = new List<User>();

            // Create users foreach users of the presentation plan
            presentationPlan.ForEach(p =>_surveyCreationDTODown.Users.Add(new User() { Code = p.UserCode }));

            // Bind the _products to the Products property
            _surveyCreationDTODown.Products = _products.ToList();

            // Bind a list of UserProducts to the UserProducts property
            _surveyCreationDTODown.UserProducts = new List<UserProduct>();

            // Foreach User presents in the presentation plan
            foreach (var surveyUser in presentationPlan)
            {
                // Create products foreach users
                for (int i = 0; i < surveyUser.Products.Count(); i++)
                {
                    // Create a userProduct
                    var userProduct = new UserProduct()
                    {
                        User = new User() { Code = surveyUser.UserCode },
                        Position = i,
                        Product = _products.FirstOrDefault(x => x.Code == surveyUser.Products.ToList()[i]),
                    };

                    // Add the product to the userProducts
                    _surveyCreationDTODown.UserProducts.Add(userProduct);

                }
            }

            // Call the CreateSurvey() method from the surveyService
            await _surveyService.CreateSurvey(_surveyCreationDTODown,HttpContext.Request.Cookies["Token"]);

            // Return to View Surveys
            return RedirectToAction("index", "surveys");
        }
        #endregion

        #region EditPage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>Redirect to the Index() method in Surveys controller</returns>
        public async Task<IActionResult> EditPage(int surveyId = 0)
        {
            try
            {
                // If the id of the survey is equal to 0
                if (surveyId == 0)
                    // throw an exception
                    throw new Exception("Survey Id can't be 0");

                // Call GetSurvey() method from surveyService
                var survey = await _surveyService.GetSurvey(surveyId, HttpContext.Request.Cookies["Token"]);

                // If we don't get a survey from the previous call
                if (survey != null)
                {
                    // Create a list of the Wrapper Question / Instruction
                    var questionInstructions = new List<QuestionInstructionWrapper>();

                    // Add instructions to the list
                    questionInstructions.AddRange(survey.Instructions?.Select(x => new QuestionInstructionWrapper(x)));
                    // Add questions to the list
                    questionInstructions.AddRange(survey.Questions?.Select(x => new QuestionInstructionWrapper(x)));

                    // Order by Position
                    questionInstructions.OrderBy(y => y.Position);

                    // Call the SurveyViewModel
                    var model = new SurveyViewModel
                    {
                        Survey = survey,
                        QuestionsInstructions = questionInstructions.OrderBy(x => x.Position)
                    };

                    // Return the View Detail with the infos about the survey
                    return View("detail", model);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            // Return the view Surveys
            return RedirectToAction("index", "surveys");
        }
        #endregion

        #region EditSurvey
        /// <summary>
        /// Allow to edit a survey
        /// </summary>
        /// <param name="survey">The survey you want to edit</param>
        /// <param name="inputQuestionInstruction">Libelle of the questions / instructions</param>
        /// <param name="inputListPosition">Positions of the questions / instructions</param>
        /// <returns>Return the index() method of the Surveys controller</returns>
        public async Task<IActionResult> EditSurvey(Survey survey, List<string>? inputQuestionInstruction, List<string> inputListPosition)
        {
            // Create a list of questions
            var questions = new List<Question>();
            // Create a list of instructions
            var instructions = new List<Instruction>();

            // Pour chaques questions
            for (int i = 0; i <= inputQuestionInstruction?.Count()-1; i++)
            {
                // If the 'libelle' is equal to 'question'
                if (inputListPosition?[i].ToLower() == "question")
                {
                    // Create a question
                    var question = new Question()
                    {
                        Libelle = inputQuestionInstruction[i],
                        Position = i
                    };

                    // Add the question to the end of the list
                    questions.Add(question);
                }
                else
                {
                    var instructionStatusCode = 1; // Set status to default

                    // Assign a status code
                    switch (inputListPosition?[i].ToLower())
                    {
                        case "introduction":
                            instructionStatusCode = 0;
                            break;
                        case "conclusion":
                            instructionStatusCode = 2;
                            break ;;
                    }

                    // Create an instruction
                    var instruction = new Instruction()
                    {
                        Libelle = inputQuestionInstruction[i],
                        Position = i,
                        Status = instructionStatusCode
                    };

                    // Add the instruction to the end of the list
                    instructions.Add(instruction);
                }
            }

            // Survey before changes
            var baseSurvey = await _surveyService.GetSurvey(survey.Id, HttpContext.Request.Cookies["Token"]);

            // Survey after changes
            var surveyToUpdate = new Survey()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = new List<Question>(),
                Instructions = new List<Instruction>()
            };

            // Get each questions of the base Survey
            foreach (var question in questions)
            {
                // Get the questions
                var baseQuestion = baseSurvey.Questions?.FirstOrDefault(q => q.Libelle == question.Libelle);

                // If it gets some questions
                if (baseQuestion != null)
                {
                    // Get the new position
                    baseQuestion.Position = question.Position;
                    // Modify the position
                    surveyToUpdate.Questions?.Add(baseQuestion);
                }
                else
                {
                    // Add the new question
                    surveyToUpdate.Questions?.Add(question);
                }
            }

            // Get each instructions of the base Survey
            foreach (var instruction in instructions)
            {
                // Get the instructions
                var baseInstruction = baseSurvey.Instructions?.FirstOrDefault(i => i.Libelle == instruction.Libelle);

                // If it gets some instructions
                if (baseInstruction != null)
                {
                    // Get the new position
                    baseInstruction.Position = instruction.Position;
                    // Modify the position
                    surveyToUpdate.Instructions?.Add(baseInstruction);
                }
                else
                {
                    // Add the new instuction
                    surveyToUpdate.Instructions?.Add(instruction);
                }
            }

            // Update the survey
            var resultCreation = await _surveyService.UpdateSurvey(surveyToUpdate, HttpContext.Request.Cookies["Token"]);

            // Return to view Surveys
            return RedirectToAction("index", "surveys");
        }
        #endregion

        #region DownloadUsersUrls
        /// <summary>
        /// Download users urls
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>A downloaded .csv file with all the users urls for the survey</returns>
        public async Task<FileResult> DownloadUsersUrls(int surveyId)
        {
            // Get all the urls
            var usersUrls = await _userService.GetUsersUrls(surveyId, HttpContext.Request.Cookies["Token"]);

            // Write the content of the csv file
            var csvFileStream = await _fileService.WriteCsvFile(usersUrls);

            // Return the new .csv file with the content
            return new FileStreamResult(csvFileStream, "text/plain")
            {
                FileDownloadName = "liens utilisateurs.csv"
            };
        }
        #endregion

        #region DownloadUsersAnswers
        /// <summary>
        /// Download the users answers
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <returns>A downloaded .csv file with all the users answers for the survey</returns>
        public async Task<FileResult> DownloadUsersAnswers(int surveyId)
        {
            // Get all the answers for the survey
            var answers = await _answerService.GetSurveyAnswers(surveyId, HttpContext.Request.Cookies["Token"]);

            // Write the content of the .csv file
            var csvFileStream = await _fileService.WriteCsvFile(answers);

            // Return the new .csv file with the content
            return new FileStreamResult(csvFileStream, "text/plain")
            {
                FileDownloadName = "réponses utilisateurs.csv"
            };
        }
        #endregion

        #region Deploy
        /// <summary>
        /// Deploy the survey. It will change the status of the survey and will allow us to Download Urls
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="action">Deploy</param>
        /// <returns>Return the EditPage</returns>
        public async Task<IActionResult> Deploy(int surveyId, string action)
        {
            // Call the Deploy() method in the survey service
            var deploySurvey = await _surveyService.Deploy(surveyId, action, HttpContext.Request.Cookies["Token"]);
            // Return the EditPage
            return await EditPage(surveyId);
        }
        #endregion

        #region Undeploy
        /// <summary>
        /// Undeploy the survey. It will change the status of the survey
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="action">Undeploy</param>
        /// <returns>Return the EditPage</returns>
        public async Task<IActionResult> Undeploy(int surveyId, string action)
        {
            // Call the Undeploy() method in the survey service
            var deploySurvey = await _surveyService.Undeploy(surveyId, action, HttpContext.Request.Cookies["Token"]);
            // Return the EditPage
            return await EditPage(surveyId);
        }
        #endregion
    }
}