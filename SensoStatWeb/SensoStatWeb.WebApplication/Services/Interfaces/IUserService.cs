using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all the urls for the users
        /// </summary>
        /// <param name="surveyId">The id of the survey</param>
        /// <param name="token">The token in the cookies</param>
        /// <returns>Return all the urls of the users</returns>
        Task<List<UserUrlDTODown>> GetUsersUrls(int surveyId, string token);
    }
}
