using SensoStatWeb.Models.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SensoStatWeb.Business.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(Administrator administrator);

        List<User> GenerateJwtTokenForUser(List<User> users);

        Task<JwtSecurityToken?> ReadJwtToken(string token);
    }
}
