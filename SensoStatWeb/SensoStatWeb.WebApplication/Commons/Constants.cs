using System;
namespace SensoStatWeb.WebApplication.Commons
{
    public class Constants
    {
        #region API      
        public const string BaseUrlApi = "https://appsensostatapi.azurewebsites.net/";
        //public const string BaseUrlApi = "https://localhost:7019/";
        #endregion

        #region Endpoints
        /// <summary>
        /// Endpoint GetSurveys() - UpdateSurvey() - CreateSurvey()
        /// </summary>
        public const string SurveyEndpoint = $"{BaseUrlApi}survey";

        /// <summary>
        /// Endpoint with SurveyId
        /// </summary>
        public const string SurveyIdEndpoint = $"{SurveyEndpoint}/SurveyId";
        #endregion
    }
}