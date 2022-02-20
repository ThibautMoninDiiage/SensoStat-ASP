using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        public List<Survey> GetAllSurveys();

        void CreateSurvey(Survey survey);

        void UpdateSurvey(Survey survey);

        void DeleteSurvey(Survey survey);
    }
}
