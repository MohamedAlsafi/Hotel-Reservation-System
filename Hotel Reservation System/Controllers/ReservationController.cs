using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.ViewModels;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Filter;
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

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.CreateReservation })]
        [HttpPost]
        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> CreateReservation(CreateReservationDto reservationDto, int CustomerId)
        {
            if (reservationDto is null)
                return BadRequest(new ApiExcaptionResponse(400));
            if (CustomerId == 0)
                return BadRequest(new ApiExcaptionResponse(400));
            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
            {
                return BadRequest(new ResponseViewModel<ReservationViewModel>(false, "Check-in date must be earlier than check-out date", null));
            }
            if (reservationDto.CheckInDate < System.DateTime.Now)
            {
                return BadRequest(new ResponseViewModel<ReservationViewModel>(false, "Check-in date must be later than today", null));
            }
            if (reservationDto.CheckOutDate < System.DateTime.Now)
            {
                return BadRequest(new ResponseViewModel<ReservationViewModel>(false, "Check-out date must be later than today", null));
            }
            if (reservationDto.CheckOutDate == reservationDto.CheckInDate)
            {
                return BadRequest(new ResponseViewModel<ReservationViewModel>(false, "Check-out date must be later than check-in date", null));
            }


            var result = await _reservationService.CreateReservationAsync(reservationDto, CustomerId);
            return Ok(result);
        }
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.CancelReservation })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel<bool>>> CancelReservation(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }


        [HttpGet("{id}")]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetReservationById })]

        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> GetReservationById(int id)
        {
            var result = await _reservationService.GetReservationByIdAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpPut("{id}")]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.UpdateReservation })]

        public async Task<ActionResult<ResponseViewModel<ReservationViewModel>>> UpdateReservation( UpdateReservationDto reservationDto)
        {
            if (reservationDto is null)
                return BadRequest(new ApiExcaptionResponse(400));
            var result = await _reservationService.UpdateReservationAsync( reservationDto);
            return StatusCode(result.Success ? 200 : 400, result);


        }
    }
}
