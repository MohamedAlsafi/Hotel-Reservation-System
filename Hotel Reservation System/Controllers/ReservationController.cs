using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.ViewModels;
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
        public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {
            if(reservationDto == null) return BadRequest(new ApiExcaptionResponse(400 , "Invalid Reservation Data"));
            var MappedReservation= _mapper.Map<CreateReservationDto>(reservationDto);
             var Reservation=await _reservationService.CreateReservationAsync(MappedReservation);
            if(Reservation is null) return BadRequest(new ApiExcaptionResponse(400));
            return CreatedAtAction(nameof(GetReservationById), new { id = MappedReservation.RoomId }, _mapper.Map<ReservationDto>(reservationDto));
        }

        [HttpGet("GetReservationById/{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(int id)
        {
            if(id <= 0) return BadRequest("Invalid reservation id.");
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation is null) return NotFound(new ApiExcaptionResponse(404, "Reservation not found"));
            var mappedReservation = _mapper.Map<ReservationDto>(reservation);
            return Ok(mappedReservation);
        }   

        [HttpPut("UpdateReservstion/{id}")]
        public async Task<ActionResult<ReservationViewModel>> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
            if(id <= 0) return BadRequest("Invalid reservation id.");
            if (reservationDto is null) return BadRequest(new ApiExcaptionResponse(400 , "Invalid data"));
            var updated = await _reservationService.UpdateReservationAsync(id, reservationDto);

            if (updated is null) return NotFound(new ApiExcaptionResponse(404, "There are no updated data."));

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationViewModel>> DeleteReservation(int id)
        {
            if(id <= 0) return BadRequest("Invalid reservation id.");
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation is null) return NotFound(new ApiExcaptionResponse(404 ,"Reservation not found"));

            var mapped =  await _reservationService.CancelReservationAsync(id);
      
            return Ok();
        }
    }
}
