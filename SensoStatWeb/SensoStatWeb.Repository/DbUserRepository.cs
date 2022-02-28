using System;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
	public class DbUserRepository : IUserRepository
	{
        private readonly SensoStatDbContext _context;
        public DbUserRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}

