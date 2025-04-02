using Hotel.Core.Data.Context;
using Hotel.Core.Entities;
using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            var existingScheme = Services.FirstOrDefault(s =>
           s.ServiceType == typeof(JwtBearerOptions) &&((JwtBearerOptions)s.ImplementationInstance)?.ForwardAuthenticate == JwtBearerDefaults.AuthenticationScheme);

            if (existingScheme == null)
            {
                Services.AddAuthentication(opt => 
                { 
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(Options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

                };
                Options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claimsIdentity = context?.Principal?.Identity as ClaimsIdentity;
                        if (claimsIdentity != null && claimsIdentity.HasClaim(claim => claim.Type == ClaimTypes.Role && claim.Value == Roles.Customer.ToString()))
                        {
                            var role = Roles.Customer; // or any default role
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        else
                        {
                            var role = Roles.Receptionist;
                            claimsIdentity?.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        return Task.CompletedTask;
                    }
                };

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            }); ;
            }
            Services.AddAuthorization(options =>
            {
                options.AddPolicy("ElavatedRights", policy => policy.RequireRole("Customer", "Receptionist" , "Admin" , "Accountant"));
            });
            Services.AddScoped<ITokenService, TokenService>();
            return Services;
        }
    }
}
