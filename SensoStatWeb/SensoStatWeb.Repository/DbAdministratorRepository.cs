using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SensoStatWeb.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;
using System.Security.Cryptography;

namespace SensoStatWeb.Repository
{
    public class DbAdministratorRepository : IAdministratorRepository
    {
        private readonly SensoStatDbContext _context;
        private readonly IJwtService _jwtService;
        public DbAdministratorRepository(SensoStatDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<Administrator> GetAdministrator(int id)
        {
            return _context.Administrators.FirstOrDefault(a => a.Id == id);
        }

        public async Task<AdministratorTokenDTODown> Login(string username, string password)
        {
            var admin = _context.Administrators.SingleOrDefault(a => a.UserName == username);

            if(admin == null)
            {
                return null;
            }

            var hashedPassword = await HashPasswordWithSalt(password, Convert.FromBase64String(admin.Salt));

            if (admin.Password == hashedPassword)
            {
                var token = _jwtService.GenerateJwtToken(admin);
                return new AdministratorTokenDTODown() { Administrator = admin, Token = token };
            }
            else
            {
                return null;
            }
        }

        public async Task<Administrator> Register(Administrator administrator)
        {
            try
            {
                var salt = await GenerateSalt();
                var hashedPassword = await HashPasswordWithSalt(administrator.Password, salt);
                administrator.Password = hashedPassword;
                administrator.Salt = Convert.ToBase64String(salt);
                _context.Administrators.Add(administrator);
                _context.SaveChanges();

                return administrator;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public async Task<byte[]> GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public async Task<string> HashPasswordWithSalt(string password, byte[] salt)
        {
            // obtenir une clé de 256-bit (en utilisant HMACSHA256 sur 100,000 itérations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}