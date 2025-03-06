using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.ViewModels;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
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
        public async Task<ReservationDto> MakePaymentAsync(int customerId, int ReservationId)
        {
            if(ReservationId ==0) return null;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
          var Reservation=await _reservationService.GetReservationByIdAsync(customerId);
            if ( Reservation is null) return null;
            //Amount = total amount of reservation + discout if any
            var Amount = 0m;
            if(Reservation.Discount != 0m)
            {
                Amount = Reservation.TotalPrice - Reservation.Discount;
            }
            else
            {
                Amount = Reservation.TotalPrice;
            }
            var SubTotal = Reservation.PaymentStatus == "Paid" ? Amount : 0m;
            var Service = new PaymentIntentService();

            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Reservation.PaymentIntentId))
            {

                var options = new PaymentIntentCreateOptions
               
                {
                    Amount = (long?)Amount ,
                    Currency = "usd",
                  PaymentMethodTypes = new List<string>()
                { "Card"},
                };


             paymentIntent=   await  Service.CreateAsync(options);
                Reservation.PaymentIntentId = paymentIntent.Id;
                Reservation.PaymentStatus = "Paid";
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions
                {
                    Amount = (long?)Amount,
                };
               var updatedPaymentIntent =  await Service.UpdateAsync(Reservation.PaymentIntentId , Options);
               Reservation.PaymentIntentId = updatedPaymentIntent.Id;

            }
             
            await _reservationService.UpdateReservationAsync(customerId, Reservation);
            return Reservation;

          

        }
    }
}
