using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product>? CreateProduct(Product product);
        Task<List<Product>> CreateProduct(List<Product> products);

        Task<List<Product>>? GetAllProducts();
    }
}
