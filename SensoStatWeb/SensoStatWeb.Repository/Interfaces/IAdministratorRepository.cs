using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<AdministratorTokenDTODown> Login(string username, string password);

        Task<Administrator> GetAdministrator(int id);

        Task<Administrator> Register(Administrator administrator);

        Task<byte[]> GenerateSalt();

        Task<string> HashPasswordWithSalt(string password, byte[] salt);
    }
}
