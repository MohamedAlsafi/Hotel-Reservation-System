using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.ViewModels;
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

        public ReservationController(IReservationService reservationService, IMapper mapper )
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {
            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
            {
                return BadRequest(new ResponseViewModel<ReservationViewModel>(false, "Check-in date must be earlier than check-out date", null));
            }

            var result = await _reservationService.CreateReservationAsync(reservationDto);
            return StatusCode(result.Success ? 201 : 400, result);
        }
        [Authorize(Roles = "Staff")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel<bool>>> CancelReservation(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> GetReservationById(int id)
        {
            var result = await _reservationService.GetReservationByIdAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
            var result = await _reservationService.UpdateReservationAsync(id, reservationDto);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [Authorize(Roles = "User")]

        [HttpPost("MakeReservation")]

        public async Task<ActionResult<ReservationDto>> MakeReservationForSpecificCustomer(ReservationDto reservationDto)
        {
            if (reservationDto is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            var mappedReservation = _mapper.Map<CreateReservationDto>(reservationDto);
            var reservation = await _reservationService.CreateReservationAsync(mappedReservation);
            if (reservation is null) return BadRequest(new ApiExcaptionResponse(400, "Invalid Reservation Data"));
            return Ok(reservation);
        }
    }
}
