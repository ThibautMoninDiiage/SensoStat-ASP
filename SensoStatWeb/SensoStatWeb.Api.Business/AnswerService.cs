using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Api.Business
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductServices _productService;
        private readonly IJwtService _jwtService;
        private readonly IUserServices _userService;
        private readonly IQuestionRepository _questionRepository;
        private readonly ISurveyServices _surveyServices;

        public AnswerService(IAnswerRepository answerRepository,
            IUserRepository userRepository,
            IProductServices productService,
            IJwtService jwtService,
            IUserServices userServices,
            IQuestionRepository questionRepository,
            ISurveyServices surveyServices)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _productService = productService;
            _jwtService = jwtService;
            _userService = userServices;
            _questionRepository = questionRepository;
            _surveyServices = surveyServices;
        }

        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId)
        {
            var answers = await _answerRepository.GetSurveyAnswers(surveyId);

            var result = answers.Select(a => new SurveyAnswersDTODown() { Answer = a.UserAnswer, Question = a.Question.Libelle, UserCode = a.User.Code });

            return result;
        }

        public async Task<Answer> CreateAnswer(AnswerDTOUp answerDTOUp)
        {
            var jsonToken = await _jwtService.ReadJwtToken(answerDTOUp.Token);
            var userId = jsonToken.Claims.FirstOrDefault(c => c.Type.Contains("id")).Value;
            var surveyId = jsonToken.Claims.FirstOrDefault(c => c.Type.Contains("primarysid")).Value;

            var result = userId.Remove(userId.Length - surveyId.Length);

            // .Substring(jsonToken.Claims.FirstOrDefault(c => c.Type.Contains("primarysid")).Value.Length);

            var answer = new Answer()
            {
                UserId = result,
                QuestionId = answerDTOUp.QuestionId,
                UserAnswer = answerDTOUp.UserAnswer,
                ProductId = answerDTOUp.ProductId,
                User = await _userService.GetUser(userId),
                Question = await _questionRepository.GetQuestion(answerDTOUp.QuestionId),
                Product = await _productService.GetProduct(answerDTOUp.ProductId)

            };

            var answerResult = await _answerRepository.CreateAnswer(answer);

            return answerResult;
        }

        public async Task<string> GetSurveyPercentageAnswers(int surveyId)
        {
            var stat = await _answerRepository.GetPercentageAnswerOfSurvey(surveyId);

            if (float.IsNaN(stat))
                return "0%";

            return stat.ToString("0.00%");
        }
    }
}
