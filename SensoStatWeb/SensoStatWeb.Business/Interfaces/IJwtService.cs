using SensoStatWeb.Models.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SensoStatWeb.Business.Interfaces
{
    public interface IJwtService
    {
        string generateJwtToken(Administrator administrator);

        List<User> generateJwtTokenForUser(List<User> users);

        Task<JwtSecurityToken?> ReadJwtToken(string token);
    }
}
