﻿using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;
        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<UserUrlDTODown>> GetUsersUrls(int surveyId, string token)
        {
            var usersUrls = await _httpService.SendHttpRequest<List<UserUrlDTODown>>($"{Constants.BaseUrlApi}user?id={surveyId}", HttpMethod.Get, bearer: token);

            return usersUrls;
        }
    }
}
