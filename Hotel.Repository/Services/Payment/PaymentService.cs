using AutoMapper;
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
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration , IReservationService reservationService , IMapper mapper)
        {
            _configuration = configuration;
            _reservationService = reservationService;
            this._mapper = mapper;
        }
        public async Task<ReservationDto> MakePaymentAsync(int RoomId)
        {
            if(RoomId == 0) return null;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
          var Reservation=await _reservationService.GetReservationByIdAsync(RoomId);
            var mappedReservation = _mapper.Map<ReservationDto>(Reservation);

            if ( Reservation is null) return null;
            //Amount = total amount of reservation + discout if any
            var Amount = 0m;
            if(mappedReservation.Discount!= 0m)
            {
                Amount = mappedReservation.TotalPrice - mappedReservation.Discount;
            }
            else
            {
                Amount = mappedReservation.TotalPrice;
            }
            var SubTotal = mappedReservation.PaymentStatus == "Paid" ? Amount : 0m;
            var Service = new PaymentIntentService();

            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(mappedReservation.PaymentIntentId))
            {

                var options = new PaymentIntentCreateOptions
               
                {
                    Amount = (long?)Amount ,
                    Currency = "usd",
                  PaymentMethodTypes = new List<string>()
                { "Card"},
                };


             paymentIntent=   await  Service.CreateAsync(options);
                mappedReservation.PaymentIntentId = paymentIntent.Id;
                mappedReservation.PaymentStatus = "Paid";
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions
                {
                    Amount = (long?)Amount,
                };
               var updatedPaymentIntent =  await Service.UpdateAsync(mappedReservation.PaymentIntentId , Options);
               mappedReservation.PaymentIntentId= updatedPaymentIntent.Id;

            }
            var updateReservationDto = _mapper.Map<UpdateReservationDto>(mappedReservation);
            await _reservationService.UpdateReservationAsync(RoomId , updateReservationDto);
            return  mappedReservation;
        }
    }
}
