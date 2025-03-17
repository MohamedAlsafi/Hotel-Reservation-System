using Hotel.Core.Data.Context;
using Hotel.Core.Entities.CustomerEntities;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hotel_Reservation_System.Helpers
{
    public static class AddIdentity
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication().AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };


            });
            Services.AddScoped<ITokenService, TokenService>();
            return Services;
        }
    }
}
