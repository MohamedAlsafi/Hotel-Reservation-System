using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.Payment;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [ApiController]
    [Route("[action]/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public ReservationController(IReservationService reservationService, IMapper mapper  , IPaymentService paymentService)
        {
            _reservationService = reservationService;
            _mapper = mapper;
            this._paymentService = paymentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CustomerReservationViewModel>> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var MappedReservation = _mapper.Map<CreateReservationDto>(reservationDto);
            await _reservationService.CreateReservationAsync(MappedReservation);
            var payment = _paymentService.MakePaymentAsync(reservationDto.RoomId);
            if (payment is null) return BadRequest(new ApiResponse(400, "Payment failed."));
            return CreatedAtAction(nameof(GetReservationById), new { id = reservationDto.RoomId },reservationDto);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);

            var reservationDto = _mapper.Map<ReservationDto>(reservation.Data);

            return Ok(new ApiResponse<ReservationDto>(true, "Reservation retrieved successfully", reservationDto));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerReservationViewModel>> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
            if (reservationDto == null) return BadRequest(new ApiResponse(400,"Invalid reservation data."));

            var updated = await _reservationService.UpdateReservationAsync(id, reservationDto);
            if (updated is null) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<CustomerReservationViewModel>> DeleteReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(new ApiResponse(404));

            await _reservationService.CancelReservationAsync(id);
            return NoContent();
        }
    }
}
