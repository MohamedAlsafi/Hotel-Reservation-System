using Hotel.Core.Data.Context;
using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Enum.HotelStaff;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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
                Options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var claimsIdentity = context?.Principal?.Identity as ClaimsIdentity;
                        if (claimsIdentity != null && claimsIdentity.)
                        {
                            var role = Roles.User; // or any default role
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                            var Role = Roles.Staff;
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, Role.ToString()));

                        }
                    }
                };
            });
            Services.AddAuthorization(options =>
            {
                options.AddPolicy("ElavatedRights", policy => policy.RequireRole("User", "Staff"));
            });
            Services.AddScoped<ITokenService, TokenService>();
            return Services;
        }
    }
}
