using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ISurveyRepository _surveyRepository;

        public ProductServices(IProductRepository productRepository,ISurveyRepository surveyRepository)
        {
            _productRepository = productRepository;
            _surveyRepository = surveyRepository;
        }

        public async Task<List<Product>>? CreateProducts(List<Product> products,Survey survey)
        {
            List<Product> createdProducts = new List<Product>();
            foreach (var product in products)
            {
                product.Survey = await _surveyRepository.GetSurvey(survey.Id);
                product.UserProducts = new List<UserProduct>();
                createdProducts.Add(await _productRepository.CreateProduct(product));
            }
            return createdProducts;
        }

        public async Task<List<Product>>? GetAllProducts()
        {
            List<Product> result = await _productRepository.GetAllProducts();
            return result;
        }
    }
}
