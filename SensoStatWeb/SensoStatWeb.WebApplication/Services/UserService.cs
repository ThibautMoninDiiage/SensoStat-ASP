using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class UserService : IUserService
    {
        #region privates
        private readonly IHttpService _httpService;
        #endregion

        #region CTOR
        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        #endregion

        #region GetUsersUrls()
        public async Task<List<UserUrlDTODown>> GetUsersUrls(int surveyId, string token)
        {
            // Get route + endpoint
            var usersUrls = await _httpService.SendHttpRequest<List<UserUrlDTODown>>($"{Constants.BaseUrlApi}user?id={surveyId}", HttpMethod.Get, bearer: token);

            // Return the users url
            return usersUrls;
        }
        #endregion
    }
}
