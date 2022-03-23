using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IAdministratorService
    {
        /// <summary>
        /// Call login method in API (POST)
        /// </summary>
        /// <param name="username">The username in the input</param>
        /// <param name="password">The password in the input</param>
        /// <returns>The administrator</returns>
        Task<AdministratorTokenDTODown> Login(string username, string password);

        /// <summary>
        /// Call Register method in API (POST)
        /// </summary>
        /// <param name="administrator">The administrator</param>
        /// <param name="token">THe token in the Cookies</param>
        /// <returns></returns>
        Task Register(AdministratorDTOUp administrator, string token);
    }
}

