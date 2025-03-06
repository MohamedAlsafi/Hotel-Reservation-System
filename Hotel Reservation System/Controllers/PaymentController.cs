﻿using AutoMapper;
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

        public PaymentController(IPaymentService paymentService )
        {
            _paymentService = paymentService;
     
        }
        [HttpPost]
        public async Task<ActionResult<PaymentProcessViewModel>> MakePaymentAsync(int ReservationId ,int customerId)
        {
            var result = await _paymentService.MakePaymentAsync(customerId, ReservationId);

            if(result is null) return BadRequest(new ApiExcaptionResponse(400));
            
            if (!string.IsNullOrEmpty(result.PaymentIntentId))
            {
                return Ok("Payment Succeeded.");
            }
            return BadRequest(new ApiExcaptionResponse(400)); 
        }

    }
}
