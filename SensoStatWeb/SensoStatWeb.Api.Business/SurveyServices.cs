using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class SurveyServices : ISurveyServices
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IAdministratorRepository _administratorRepository;
        private readonly ISurveyStateRepository _surveyStateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserServices _userServices;
        private readonly IProductServices _productServices;
        private readonly IUserProductServices _userProductServices;
        private readonly IAnswerService _answerService;

        public SurveyServices(ISurveyRepository surveyRepository,
            IAdministratorRepository administratorRepository,
            ISurveyStateRepository surveyStateRepository,
            IUserRepository userRepository, 
            IJwtService jwtService,
            IUserServices userServices,
            IProductServices productServices,
            IUserProductServices userProductServices,
            IAnswerService answerService)
        {
            _userProductServices = userProductServices;
            _productServices = productServices;
            _userServices = userServices;
            _jwtService = jwtService;
            _surveyRepository = surveyRepository;
            _administratorRepository = administratorRepository;
            _surveyStateRepository = surveyStateRepository;
            _userRepository = userRepository;
            _answerService = answerService;
        }

        public async Task<Survey> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown)
        {
            var survey = new Survey()
            {
                Name = surveyCreationDTODown.Name,
                CreatorId = 1,
                Administrator = await _administratorRepository.GetAdministrator(surveyCreationDTODown.AdminId),
                CreationDate = DateTime.Now,
                SurveyState = await _surveyStateRepository.GetSurveyState(1),
                StateId = 1,
                Users = new List<User>(),
                Instructions = new List<Instruction>(),
                Questions = new List<Question>(),
                Products = new List<Product>(),
            };

            // Add default introduction + conclusion
            survey.Instructions.Add(new Instruction() { Libelle = "Bienvenue à cette séance", Position = 1, Status = 0 });
            survey.Instructions.Add(new Instruction() { Libelle = "Merci de votre participation", Position = 2, Status = 2});

            // Create the survey in database
            var surveyResult = await _surveyRepository.CreateSurvey(survey);
            // Create all users for this survey
            var createdUsers = await _userServices.CreateUsers(surveyCreationDTODown.Users, surveyResult);
            // create all product for this survey
            var createdProducts = await _productServices.CreateProducts(surveyCreationDTODown.Products, surveyResult);

            // Finally create all user products
            await _userProductServices.CreateUserProducts(surveyCreationDTODown.UserProducts, surveyResult, createdUsers, createdProducts);

            return surveyResult;
        }

        public async Task<bool> DeleteSurvey(int id)
        {
            return await _surveyRepository.DeleteSurvey(id);
        }

        public async Task<bool> DeploySurvey(int surveyId)
        {
            var result = await _surveyRepository.DeploySurvey(surveyId);
            var userLink = await _userRepository.CreateUrl(surveyId);

            return result != false && userLink != null;
        }

        public async Task<bool> UndeploySurvey(int surveyId)
        {
            var result = await _surveyRepository.UndeploySurvey(surveyId);

            return result != false;
        }

        public async Task<List<SurveyWithStatsDtoDown>> GetAllSurveys()
        {
            var surveys =await _surveyRepository.GetAllSurveys();

            var surveyDtoWithStats = surveys.Select(s =>
            new SurveyWithStatsDtoDown()
            {
                Survey = new SurveyLiteDTODown(s),
                PercentageOfCompletion = _answerService.GetSurveyPercentageAnswers(s.Id).Result
            }).ToList();

            return surveyDtoWithStats;
        }

        public async Task<Survey> GetSurveyForAdministrator(int id)
        {
            return await _surveyRepository.GetSurveyForAdministrator(id);
        }

        //public async Task<Survey> GetSurvey(int id)
        //{
        //    var survey = await _surveyRepository.GetSurvey(id);
        //    return survey;
        //}

        public async Task<Survey> GetSurvey(string token)
        {
            var jsonToken = await _jwtService.ReadJwtToken(token);

            var userSurveyId = jsonToken.Claims.FirstOrDefault(c => c.Type.Contains("id")).Value;

            var survey = await _surveyRepository.GetSurveyByUserId(userSurveyId);

            survey.Products = survey.UserProducts.OrderBy(up => up.Position).Select(up => up.Product).ToList();
            survey.UserProducts = null;

            // If the survey is not deploy return null
            return survey.StateId == 1 ? null : survey;
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            Survey result = await _surveyRepository.UpdateSurvey(survey);
            return result;
        }
    }
}
