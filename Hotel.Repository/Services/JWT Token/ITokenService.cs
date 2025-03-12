using Hotel.Core.Entities.customer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.OfferService.JWT_Token
{
    public interface ITokenService
    {
        public Task<string> GenerateTokenAsync(Customer customer , UserManager<Customer> userManager);
    }
}
