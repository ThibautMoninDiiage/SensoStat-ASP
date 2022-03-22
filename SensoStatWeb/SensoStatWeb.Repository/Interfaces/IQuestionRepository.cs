using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
	public interface IQuestionRepository
	{
		Task<List<Question>>? GetAllQuestions();

		Task<Question>? CreateQuestion(Question question);

		Task<bool>? DeleteQuestion(int id);

		Task<Question>? GetQuestion(int id);
	}
}