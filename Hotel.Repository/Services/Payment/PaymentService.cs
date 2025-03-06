using Hotel.Repository.Services.ReservationService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IReservationService _reservationService;

        public PaymentService(IConfiguration configuration , IReservationService reservationService)
        {
            _configuration = configuration;
            _reservationService = reservationService;
        }
        public Task<bool> MakePaymentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
