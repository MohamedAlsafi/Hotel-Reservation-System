using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Enum.HotelStaff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.OfferService.JWT_Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync(Customer user, string userName)
        {
            var Key = _configuration["Jwt:Key"];
            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _configuration["Jwt:Audience"],
                Claims = new Dictionary<string, object>
                {
                    { ClaimTypes.NameIdentifier, user.FirstName },
                    { ClaimTypes.Email, user.Email },
                    { ClaimTypes.MobilePhone, user.PhoneNumber },
                    // Convert enum to string before adding to claims
                    { ClaimTypes.Role, Roles.User.ToString() }
                },

                Expires = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"]
            };

            var token = TokenHandler.CreateToken(TokenDescriptor);
            return await Task.FromResult(TokenHandler.WriteToken(token));
        }

        public Task<string> GetTokenAsyncForHotelStaff(int userId, string userName, HotelStaffRole role)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, Roles.Staff.ToString())
            };

            var authKeyString = _configuration["Jwt:Key"];
            if (authKeyString?.Length < 32)
            {
                throw new ArgumentOutOfRangeException("Jwt:Key", "The key size must be at least 256 bits (32 bytes).");
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKeyString));
            var Token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }
    }
}