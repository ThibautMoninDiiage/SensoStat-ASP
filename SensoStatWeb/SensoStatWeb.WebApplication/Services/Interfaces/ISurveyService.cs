using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        /// <summary>
        /// Call GetSurveys() method in API (GET)
        /// </summary>
        /// <param name="token">The token in the cookies</param>
        /// <returns>The surveys</returns>
        Task<List<Survey>> GetSurveys(string token = "");

        /// <summary>
        /// Call GetSurvey() method in API (GET)
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>The survey</returns>
        Task<Survey> GetSurvey(int surveyId, string token = "");

        /// <summary>
        /// Create a survey
        /// </summary>
        /// <param name="surveyCreationDTODown">The survey</param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>The created survey</returns>
        Task<SurveyCreationDTODown> CreateSurvey(SurveyCreationDTODown surveyCreationDTODown, string token = "");

        /// <summary>
        /// Delete a survey
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="token">The token in the cookies</param>
        /// <returns></returns>
        Task<bool> DeleteSurvey(int surveyId, string token = "");

        /// <summary>
        /// Update a survey
        /// </summary>
        /// <param name="survey">The survey</param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>The updated survey</returns>
        Task<Survey> UpdateSurvey(Survey survey, string token = "");

        /// <summary>
        /// Deploy a survey
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="action"></param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>Return the deployed survey</returns>
        Task<bool> Deploy(int surveyId, string action ,string token = "");

        /// <summary>
        /// Undeploy a survey
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="action"></param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>Return the undeployed survey</returns>
        Task<bool> Undeploy(int surveyId, string action, string token = "");
    }
}