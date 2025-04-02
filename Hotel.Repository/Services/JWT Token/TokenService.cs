using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Repository.Services.OfferService.JWT_Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> GetTokenAsync(int userId, string userName, Roles? role)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role,((int) role.GetValueOrDefault()).ToString())
            };

            var authKeyString = _configuration["Jwt:Key"];
            if (authKeyString?.Length < 32)
            {
                throw new ArgumentOutOfRangeException("Jwt:Key", "The key size must be at least 256 bits (32 bytes).");
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKeyString));
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(AuthClaims),
                Expires = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                SigningCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var Token = new JwtSecurityTokenHandler().CreateToken(TokenDescriptor);
            if (Token is null) throw new ArgumentNullException("Token", "Token is null");
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(Token));
            
        }
    }
}