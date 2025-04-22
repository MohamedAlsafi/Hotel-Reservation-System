using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.Payment;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("MakePayment")]
        public async Task<ResponseViewModel<PaymentProcessViewModel>> MakePaymentAsync(int ReservationId, int customerId)
        {
            if (ReservationId == 0)
            {
                return new ResponseViewModel<PaymentProcessViewModel>(
                    success: false,
                    message: "Invalid Reservation ID",
                    data: null!,
                    errorCode: null
                );
            }

            var result = await _paymentService.MakePaymentOrUpdateAsync(customerId, ReservationId);
            if (result is null)
            {
                return new ResponseViewModel<PaymentProcessViewModel>(
                    success: false,
                    message: "Payment processing failed",
                    data: null!,
                    errorCode: null
                );
            }

            if (!string.IsNullOrEmpty(result.PaymentIntentId))
            {
                return new ResponseViewModel<PaymentProcessViewModel>(
                    success: true,
                    message: "Payment Succeeded",
                    data: new PaymentProcessViewModel
                    {
                        PaymentIntentId = result.PaymentIntentId,
                        PaymentStatus = result.PaymentStatus
                    },
                    errorCode: null
                );
            }

            return new ResponseViewModel<PaymentProcessViewModel>(
                success: false,
                message: "Payment failed",
                data: null!,
                errorCode: null
            );
        }
    }
}
