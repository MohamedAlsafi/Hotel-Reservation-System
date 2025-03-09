using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.Error;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [ApiController]
    [Route("[action]/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper )
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {
           // var reservation = _mapper.Map<Reservation>(reservationDto);
            var MappedReservation= _mapper.Map<CreateReservationDto>(reservationDto);
            await _reservationService.CreateReservationAsync(MappedReservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = MappedReservation.RoomId }, _mapper.Map<ReservationDto>(reservationDto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);

            var reservationDto = _mapper.Map<ReservationDto>(reservation.Data);

            return Ok(new ApiResponse<ReservationDto>(true, "Reservation retrieved successfully", reservationDto));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
            if (reservationDto == null) return BadRequest(new ApiResponse(400,"Invalid reservation data."));

            var updated = await _reservationService.UpdateReservationAsync(id, reservationDto);
            if (updated is null) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(new ApiResponse(404));

            await _reservationService.CancelReservationAsync(id);
            return NoContent();
        }
    }
}
