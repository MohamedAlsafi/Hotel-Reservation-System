using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Enum.HotelStaff;
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
        public Task<string> GetTokenAsync(Customer user, UserManager<Customer> userManager);

        public Task<string> GetTokenAsyncForHotelStaff(int userId,  string userName  ,HotelStaffRole role);


    }
}
