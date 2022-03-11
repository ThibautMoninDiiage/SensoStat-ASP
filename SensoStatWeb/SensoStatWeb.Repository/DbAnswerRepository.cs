using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class DbAnswerRepository : IAnswerRepository
    {
        public Task<IEnumerable<Answer>> GetSurveyAnswers(int surveyId)
        {
            throw new NotImplementedException();
        }
    }
}
