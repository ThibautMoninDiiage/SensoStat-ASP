using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IAdministratorService
    {
        Task<Administrator> Login(string username, string password);
    }
}

