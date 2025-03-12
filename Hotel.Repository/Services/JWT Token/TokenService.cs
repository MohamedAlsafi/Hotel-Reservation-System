using Hotel.Core.Data.Configuration;
using Hotel.Core.Entities.customer;
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
            this._configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(Customer customer, UserManager<Customer> userManager)
        {
            if(customer is null)
            {
                return null!;
            }
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.UserName),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.NameIdentifier, customer.Id)
            };

            var Roles = await userManager.GetRolesAsync(customer);
            foreach (var role in Roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
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
            return  new JwtSecurityTokenHandler().WriteToken(Token);


        }
    }
}
