using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface ISurveyServices
    {
        /// <summary>
        /// This method allow one admin to get all survey available in the database
        /// </summary>
        /// <returns>A list of all surveys</returns>
        Task<List<SurveyWithStatsDtoDown>> GetAllSurveys();
        ///// <summary>
        ///// This method return one survey 
        ///// </summary>
        ///// <param name="id">The id of the survey</param>
        ///// <returns>One survey or null</returns>
        //Task<Survey> GetSurvey(int id);

        Task<Survey> GetSurveyForAdministrator(int id);

        /// <summary>
        /// This method allow one user to get a survey with his token
        /// </summary>
        /// <param name="token">The user token</param>
        /// <returns>One survey or null if not found</returns>
        Task<Survey> GetSurvey(string token);

        Task<Survey> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown);

        Task<Survey> UpdateSurvey(Survey survey);

        Task<bool> DeleteSurvey(int id);

        Task<bool> DeploySurvey(int surveyId);

        Task<bool> UndeploySurvey(int surveyId);
    }
}
