using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.ViewModels;
using Microsoft.Extensions.Configuration;
using Stripe;


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
        public async Task<PaymentProcessViewModel?> MakePaymentOrUpdateAsync(int customerId, int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
                return null;

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(reservation. * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Description = $"Hotel reservation payment for customer {customerId}",
                Metadata = new Dictionary<string, string>
        {
            { "ReservationId", reservationId.ToString() },
            { "CustomerId", customerId.ToString() }
        }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            if (paymentIntent.Status == "succeeded")
            {
                reservation.PaymentStatus = "Success";
                await _reservationService.UpdateReservationAsync(reservation);

                return new PaymentProcessViewModel
                {
                    PaymentIntentId = paymentIntent.Id,
                    PaymentStatus = paymentIntent.Status
                };
            }

            return null;
        }
    }
}
