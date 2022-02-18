using SensoStatApi.Models;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        public List<Survey> GetAllSurveys();
    }
}
