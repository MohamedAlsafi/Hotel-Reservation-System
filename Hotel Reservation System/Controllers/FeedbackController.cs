using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.FeedbackServices;
using Hotel.Repository.Services.ReservationService;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Filter;
using Hotel_Reservation_System.ViewModels.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public FeedbackController(IFeedbackService feedbackService, IMapper mapper , IReservationService reservationService)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
            this._reservationService = reservationService;
        }

        [HttpPost]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.AddFeedback })]

        public async Task<ResponseViewModel<FeedbackResponseViewModel>> AddFeedback(AddFeedbackViewModel model)
        {
            var feedback = _mapper.Map<AddFeedbackDto>(model);
            var resultDto = await _feedbackService.AddFeedbackAsync(feedback);
            var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(resultDto);

            return new ResponseViewModel<FeedbackResponseViewModel>(true, "Feedback submitted successfully", responseViewModel);
        }

        [HttpGet]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetAllFeedback})]
        public async Task<ResponseViewModel<IEnumerable<FeedbackResponseViewModel>>> GetAllFeedback( [FromQuery] string? fromDate = null, [FromQuery] string? toDate = null)
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            var responseViewModels = _mapper.Map<IEnumerable<FeedbackResponseViewModel>>(feedbacks);

            return new ResponseViewModel<IEnumerable<FeedbackResponseViewModel>>(true, "Feedbacks retrieved successfully", responseViewModels);
        }


        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetFacilityById })]
        [HttpGet("{id}")]
        public async Task<ResponseViewModel<FeedbackResponseViewModel>> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(feedback);
            return new ResponseViewModel<FeedbackResponseViewModel>(true, "Feedback retrieved successfully", responseViewModel);
        }

        [HttpPost("respond")]
       [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.RespondToFeedback })]
        public async Task<ResponseViewModel<FeedbackResponseViewModel>> RespondToFeedback( FeedbackToResponseViewModel model)
        {
            var responseDto = _mapper.Map<FeedbackResponseDto>(model);
            var updatedFeedback = await _feedbackService.RespondToFeedbackAsync(responseDto.StaffId,responseDto);
            var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(updatedFeedback);
            return new ResponseViewModel<FeedbackResponseViewModel>(true, "Response added successfully", responseViewModel);
        }

        [HttpGet("user/{userId}")]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetFeedbackByCustomerId })]

        public async Task<ResponseViewModel<List<FeedbackResponseViewModel>>> GetFeedbackByUserId(int userId)
        {
            var feedbacks = await _feedbackService.GetFeedbackByUserIdAsync(userId);
            var responseViewModels = _mapper.Map<List<FeedbackResponseViewModel>>(feedbacks);
            return ResponseViewModel<List<FeedbackResponseViewModel>>.SuccessResult(responseViewModels, "Feedbacks retrieved successfully");
        }

     
       
    }
}
