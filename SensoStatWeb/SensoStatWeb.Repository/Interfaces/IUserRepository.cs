using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<User>? GetUser(string id);

		Task<User>? CreateUser(User user);
		Task<List<User>> CreateUser(IEnumerable<User> users);

		Task<List<User>>? GetUsers();

		Task<List<User>>? CreateUrl(int id);
	}
}

