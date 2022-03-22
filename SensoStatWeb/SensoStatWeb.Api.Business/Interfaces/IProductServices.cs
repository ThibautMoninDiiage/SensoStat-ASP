using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IProductServices
    {
        Task<List<Product>>? CreateProducts(List<Product> products,Survey survey);

        Task<List<Product>>? GetAllProducts();

        Task<Product>? GetProduct(int id);
    }
}
