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

        public PaymentService(IConfiguration configuration, IReservationService reservationService, IMapper mapper)
        {
            _configuration = configuration;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<ReservationDto> MakePaymentOrUpdateAsync(int customerId, int reservationId)
        {
            var reservationResponse = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservationResponse == null || !reservationResponse.Success || reservationResponse.Data == null)
                return null;

            var reservation = reservationResponse.Data;

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(reservation.TotalPrice * 100),
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
                reservation.PaymentStatus = PaymentStatus.Paid;

                var updateReservationDto = new UpdateReservationDto
                {
                    Id = reservation.Id,
                    CheckInDate = reservation.StartDate,
                    CheckOutDate = reservation.EndDate,
                    RoomId = reservation.RoomId,
                    PaymenyIntentId = paymentIntent.Id,
                    PaymentStatus = reservation.PaymentStatus,
                    TotalPrice = reservation.TotalPrice,
                };

                await _reservationService.UpdateReservationAsync(updateReservationDto);

                return new ReservationDto
                {
                    Id = reservation.Id,
                    CheckInDate = reservation.StartDate,
                    CheckOutDate = reservation.EndDate,
                    RoomId = reservation.RoomId,
                    PaymentStatus = reservation.PaymentStatus,
                    TotalPrice = reservation.TotalPrice
                };
            }

            return null;
        }
    }
}
