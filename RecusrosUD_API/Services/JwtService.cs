using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecusrosUD_API.Services
{
    public class JwtService(IConfiguration config)
    {
        private readonly string _secretKey = config["JwtSettings:SecretKey"] ?? "";
        private readonly string _issuer = config["JwtSettings:Issuer"] ?? "";
        private readonly string _audience = config["JwtSettings:Audience"] ?? "";

        public string GenerarToken(string email, int userId, bool admin)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim("userId", userId.ToString()),
            new Claim("admin", admin.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
