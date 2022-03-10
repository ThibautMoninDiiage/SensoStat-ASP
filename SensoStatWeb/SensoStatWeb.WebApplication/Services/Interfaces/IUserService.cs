using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.WebApplication.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserUrlDTODown>> GetUsersUrls(int surveyId, string token);
    }
}
