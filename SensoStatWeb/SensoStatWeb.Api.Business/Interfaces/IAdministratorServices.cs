using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IAdministratorServices
    {
        Task<AdministratorTokenDTODown>? Login(string username, string password);

        Task<Administrator>? GetAdministrator(int id);

        Task<bool> Register(AdministratorDTOUp administrator);
    }
}
