using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
	public class DbUserRepository : IUserRepository
	{
        private readonly SensoStatDbContext _context;
        private readonly IJwtService _jwtService;
        public DbUserRepository(SensoStatDbContext context,IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<User>? GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User>? CreateUser(User user)
        {
            _context.Users?.Add(user);
            _context.SaveChanges();
            return _context.Users.Where(u => u.Equals(user)).FirstOrDefault();
        }

        public async Task<List<User>>? GetUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<List<User>>? CreateUrl(int id)
        {
            var users = _context.Users.Where(u => u.SurveyId == id).ToList();
            var usersWithLink = _jwtService.generateJwtTokenForUser(users);
            _context.Users.UpdateRange(usersWithLink);
            _context.SaveChanges();
            return usersWithLink;
        }
    }
}

