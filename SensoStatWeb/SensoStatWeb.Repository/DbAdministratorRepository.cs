using SensoStatApi.Models;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class DbAdministratorRepository : IAdministratorRepository
    {
        private readonly SensoStatDbContext _context;
        public DbAdministratorRepository(SensoStatDbContext context)
        {
            _context = context;
        }
        public Administrator Login(string username, string password)
        {
            return _context.Administrators.FirstOrDefault(a => a.UserName == username && a.Password == password);
        }
    }
}