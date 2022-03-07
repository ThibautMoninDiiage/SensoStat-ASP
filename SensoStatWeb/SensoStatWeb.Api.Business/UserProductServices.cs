using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class UserProductServices : IUserProductServices
    {
        private readonly IUserProductRepository _userProductRepository;

        public UserProductServices(IUserProductRepository userProductRepository)
        {
            _userProductRepository = userProductRepository;
        }

        public async Task<List<UserProduct>>? CreateUserProducts(List<UserProduct> userProducts,Survey survey,List<User> users,List<Product> products)
        {
            List<UserProduct> createdUserProducts = new List<UserProduct>();
            foreach (var userProduct in userProducts)
            {
                userProduct.User = users.Where(u => u.Id == userProduct.User.Code + survey.Id).FirstOrDefault();
                userProduct.UserId = userProduct.User.Id;
                userProduct.Product = products.FirstOrDefault(p => p.Code == userProduct.Product.Code);
                userProduct.ProductId = userProduct.Product.Id;
                userProduct.Survey = survey;
            }

            await _userProductRepository.CreateUserProduct(userProducts);

            return createdUserProducts;
        }
    }
}
