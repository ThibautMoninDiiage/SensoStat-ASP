using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IUserProductServices
    {
        Task<List<UserProduct>>? CreateUserProducts(List<UserProduct> userProducts,Survey survey,List<User> users,List<Product> products);
    }
}
