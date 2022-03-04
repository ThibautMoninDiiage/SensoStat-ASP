using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IUserServices
    {
        Task<User>? GetUser(string id);

        Task<List<User>>? CreateUsers(List<User> users,Survey survey);

        Task<List<User>>? GetUsers();

        Task<List<User>>? CreateUrl(int id);
    }
}
