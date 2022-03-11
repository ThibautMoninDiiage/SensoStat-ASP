using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetSurveyAnswers(int surveyId);
    }
}
