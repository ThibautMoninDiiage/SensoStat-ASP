using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
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

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId)
        {
            var answers = await _answerRepository.GetSurveyAnswers(surveyId);

            var result = answers.Select(a => new SurveyAnswersDTODown() { Answer = a.UserAnswer, Question = a.Question.Libelle, UserCode = a.User.Code });

            return result;
        }
    }
}
