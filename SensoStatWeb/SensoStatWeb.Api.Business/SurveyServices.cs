using SensoStatWeb.Api.Business.Interfaces;
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

        public SurveyServices(ISurveyRepository surveyRepository,IAdministratorRepository administratorRepository,ISurveyStateRepository surveyStateRepository,IUserRepository userRepository)
        {
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
                Administrator = await _administratorRepository.GetAdministrator(1),
                CreationDate = DateTime.Now,
                SurveyState = await _surveyStateRepository.GetSurveyState(1),
                StateId = 1,
                Users = new List<User>(),
                Instructions = new List<Instruction>(),
                Questions = new List<Question>(),
                Products = new List<Product>(),
            };

            survey.Instructions.Add(new Instruction() { Libelle = "Bienvenue à cette séance", Position = 1, Status = 0 });
            survey.Instructions.Add(new Instruction() { Libelle = "Merci de votre participation", Position = 2, Status = 2});

            Survey result = await _surveyRepository.CreateSurvey(survey);
            return result;
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

        public async Task<Survey> GetSurveyByUserId(int userId)
        {
            Survey result = await _surveyRepository.GetSurveyByUserId(userId);
            return result;
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            Survey result = await _surveyRepository.UpdateSurvey(survey);
            return result;
        }
    }
}
