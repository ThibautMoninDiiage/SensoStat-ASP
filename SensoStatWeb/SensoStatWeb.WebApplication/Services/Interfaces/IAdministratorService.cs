using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IAdministratorService
    {
        Task<AdministratorTokenDTODown> Login(string username, string password);
        Task Register(AdministratorDTOUp administrator, string token);
    }
}

