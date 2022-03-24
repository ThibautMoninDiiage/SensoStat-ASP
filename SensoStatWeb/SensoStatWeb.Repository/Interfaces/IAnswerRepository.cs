using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetSurveyAnswers(int surveyId);

        Task<Answer>? CreateAnswer(Answer answer);

        Task<float> GetPercentageAnswerOfSurvey(int surveyId);
    }
}
