using System;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.Commons;
using SensoStatWeb.WebApplication.Services.Interfaces;

namespace SensoStatWeb.WebApplication.Services
{
    public class AdministratorService : IAdministratorService
    {
        #region privates
        private readonly IHttpService _httpService;
        #endregion

        #region CTOR
        public AdministratorService(IHttpService httpService)
        {
            this._httpService = httpService;

        }
        #endregion

        #region Login()
        public async Task<AdministratorTokenDTODown> Login(string username, string password)
        {
            // Get route + endpoint
            var url = $"{Constants.BaseUrlApi}administrator?login={username}&password={password}";
            // Call the SendHttpRequest() method from httpService Shared
            var administrator = await _httpService.SendHttpRequest<AdministratorTokenDTODown>(url, HttpMethod.Get);
            // Return the adminsistrator
            return administrator;
        }
        #endregion

        #region Register()
        public async Task Register(AdministratorDTOUp administrator, string token)
        {
            // Get route + endpoint
            var url = $"{Constants.BaseUrlApi}administrator";
            // Call the SendHttpRequest() from the httpService Shared
            var resultRegister = await _httpService.SendHttpRequest<object>(url, HttpMethod.Post, administrator, token);
        }
        #endregion
    }
}

