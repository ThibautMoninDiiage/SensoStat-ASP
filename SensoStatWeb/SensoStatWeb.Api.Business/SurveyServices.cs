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

        public SurveyServices(ISurveyRepository surveyRepository,
            IAdministratorRepository administratorRepository,
            ISurveyStateRepository surveyStateRepository,
            IUserRepository userRepository, 
            IJwtService jwtService,
            IUserServices userServices,
            IProductServices productServices,
            IUserProductServices userProductServices)
        {
            _userProductServices = userProductServices;
            _productServices = productServices;
            _userServices = userServices;
            _jwtService = jwtService;
            _surveyRepository = surveyRepository;
            _administratorRepository = administratorRepository;
            _surveyStateRepository = surveyStateRepository;
            _userRepository = userRepository;
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


            var surveyResult = await _surveyRepository.CreateSurvey(survey);

            var createdUsers = await _userServices.CreateUsers(surveyCreationDTODown.Users, surveyResult);

            var createdProducts = await _productServices.CreateProducts(surveyCreationDTODown.Products, surveyResult);

            var createdUserProduct = await _userProductServices.CreateUserProducts(surveyCreationDTODown.UserProducts, surveyResult, createdUsers, createdProducts);

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

        public async Task<List<Survey>> GetAllSurveys()
        {
            return await _surveyRepository.GetAllSurveys();
        }

        public async Task<Survey> GetSurvey(int id)
        {
            Survey result = await _surveyRepository.GetSurvey(id);
            return result;
        }

        public async Task<Survey> GetSurvey(string token)
        {
            var jsonToken = await _jwtService.ReadJwtToken(token);

            var surveyId = Int32.Parse(jsonToken.Claims.FirstOrDefault(c => c.Type.Contains("primarysid")).Value);

            return await _surveyRepository.GetSurvey(surveyId);
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            Survey result = await _surveyRepository.UpdateSurvey(survey);
            return result;
        }
    }
}
