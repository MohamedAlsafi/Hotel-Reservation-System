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
        public async Task<object> CreateReservation(CreateReservationDto reservationDto, int customerId)
        {
            if (reservationDto is null)
                return new ApiResponse(400, "Invalid reservation data");

            if (customerId == 0)
                return new ApiResponse(400, "Customer ID is required");

            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
                return new ApiResponse(400, "Check-in date must be earlier than check-out date");

            if (reservationDto.CheckInDate < DateTime.Now)
                return new ApiResponse(400, "Check-in date must be later than today");

            if (reservationDto.CheckOutDate < DateTime.Now)
                return new ApiResponse(400, "Check-out date must be later than today");

            var reservationVm = await _reservationService.CreateReservationAsync(reservationDto, customerId);

            if (reservationVm == null)
                return new ApiResponse(500, "Reservation could not be created");

            return new ResponseViewModel<ReservationViewModel>(true, "Reservation created successfully", reservationVm);
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
