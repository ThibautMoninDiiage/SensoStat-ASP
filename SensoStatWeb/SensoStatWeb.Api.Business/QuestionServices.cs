using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionServices(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<List<Question>>? GetAllQuestions()
        {
            List<Question> result = await _questionRepository.GetAllQuestions();
            return result;
        }

        public async Task<Question>? CreateQuestion(Question question)
        {
            Question result = await _questionRepository.CreateQuestion(question);
            return result;
        }

        public async Task<bool>? DeleteQuestion(int id)
        {
            bool result = await _questionRepository.DeleteQuestion(id);
            return result;
        }
    }
}
