using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class UUserProductRepository : IUserProductRepository
    {
        private readonly SensoStatDbContext _context;
        public UUserProductRepository(SensoStatDbContext context)
        {
            _context = context;
        }
        public async Task<UserProduct>? CreateUserProduct(UserProduct userProduct)
        {
            _context.UserProducts.Add(userProduct);
            _context.SaveChanges();
            return _context.UserProducts.FirstOrDefault(up => up.Equals(userProduct));
        }

        public async Task<List<UserProduct>>? CreateUserProduct(IEnumerable<UserProduct> userProducts)
        {
            _context.UserProducts.AddRange(userProducts);
            _context.SaveChanges();

            return userProducts.ToList();
        }
    }
}
