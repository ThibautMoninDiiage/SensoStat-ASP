using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IQuestionServices
    {
        Task<List<Question>>? GetAllQuestions();

        Task<Question>? CreateQuestion(Question question);

        Task<bool>? DeleteQuestion(int id);
    }
}
