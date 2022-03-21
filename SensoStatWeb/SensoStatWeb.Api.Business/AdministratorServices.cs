using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class AdministratorServices : IAdministratorServices
    {
        private readonly IAdministratorRepository _administratorRepository;
        public AdministratorServices(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }
        public async Task<Administrator> GetAdministrator(int id)
        {
            Administrator result = await _administratorRepository.GetAdministrator(id);
            return result;
        }

        public async Task<AdministratorTokenDTODown> Login(string username, string password)
        {
            AdministratorTokenDTODown result = await _administratorRepository.Login(username, password);
            return result;
        }

        public async Task<bool> Register(AdministratorDTOUp administrator)
        {
            var resultCreation = await _administratorRepository.Register(new Administrator(administrator));

            return resultCreation != null;
        }
    }
}
