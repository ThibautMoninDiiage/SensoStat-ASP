using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IUserProductRepository
    {
        Task<UserProduct>? CreateUserProduct(UserProduct userProduct);
        Task<List<UserProduct>>? CreateUserProduct(IEnumerable<UserProduct> userProducts);
    }
}
