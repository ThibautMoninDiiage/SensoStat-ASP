using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        List<Survey> GetAllSurveys();

        public Survey GetSurvey(int id);

        public Survey GetSurveyByUserId(int userId);

        Task<Survey> CreateSurvey(Survey survey);

        Task<Survey> UpdateSurvey(Survey survey);

        /// <summary>
        /// Method used to delete a survey.
        /// We're deleting : Instructions - Answers - Questions - UserProducts - Products - Users associated to it from the DbContext
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        Task<bool> DeleteSurvey(int id);
    }
}
