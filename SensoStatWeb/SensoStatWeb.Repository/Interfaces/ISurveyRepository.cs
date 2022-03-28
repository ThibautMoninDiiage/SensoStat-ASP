using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetAllSurveys();

        //Task<Survey>? GetSurvey(int id);

        /// <summary>
        /// Get a asurvey with instructions and questions
        /// </summary>
        /// <param name="id">the id of a survey</param>
        /// <returns></returns>
        Task<Survey> GetSurveyForAdministrator(int id);

        /// <summary>
        /// Return all informations of one survey thanks a userId (compose by userId and surveyId)
        /// </summary>
        /// <param name="userId">User id (ex : S00118)</param>
        /// <returns>All infomrations required to display one survey</returns>
        Task<Survey> GetSurveyByUserId(string userSurveyId);

        Task<Survey> CreateSurvey(Survey survey);

        Task<Survey> UpdateSurvey(Survey survey);

        /// <summary>
        /// Method used to delete a survey.
        /// We're deleting : Instructions - Answers - Questions - UserProducts - Products - Users associated to it from the DbContext
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        Task<bool> DeleteSurvey(int id);

        Task<bool> DeploySurvey(int surveyId);

        Task<bool> UndeploySurvey(int surveyId);
    }
}
