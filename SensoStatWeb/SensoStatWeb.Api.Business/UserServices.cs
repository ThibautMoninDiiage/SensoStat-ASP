using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ISurveyServices _surveyServices;

        public UserServices(IUserRepository userRepository,ISurveyServices surveyServices)
        {
            _userRepository = userRepository;
            _surveyServices = surveyServices;
        }

        public async Task<User>? GetUser(string id)
        {
            User result = await _userRepository.GetUser(id);
            return result;
        }

        public async Task<List<User>> CreateUsers(List<User> users,Survey survey)
        {
            List<User> createdUsers = new List<User>(); 
            foreach (var user in users)
            {
                user.Id = user.Code + survey.Id;
                user.Answers = new List<Answer>();
                user.Code = user.Code;
                user.UserProducts = new List<UserProduct>();
                user.Survey = survey;
            }

            return await _userRepository.CreateUser(users);
        }

        public async Task<IEnumerable<UserUrlDTODown>>? CreateUrl(int surveyId)
        {
            var users = await _userRepository.CreateUrl(surveyId);

            return users.Select(user => new UserUrlDTODown() { Code = user.Code, Url = user.Link });
        }
    }
}
