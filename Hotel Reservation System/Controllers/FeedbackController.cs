using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Repository.Services.FeedbackServices;
using Hotel_Reservation_System.ViewModels.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<FeedbackResponseViewModel>> AddFeedback(AddFeedbackViewModel model)
        {


            var feedback = _mapper.Map<AddFeedbackDto>(model);
            var resultDto = await _feedbackService.AddFeedbackAsync(feedback);
            var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(resultDto);

            return new ApiResponse<FeedbackResponseViewModel>(true, "Feedback submitted successfully", responseViewModel);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<ApiResponse<List<FeedbackResponseViewModel>>> GetAllFeedback()
        {
            try
            {
                var feedbacks = await _feedbackService.GetAllFeedbackAsync();
                var responseViewModels = _mapper.Map<List<FeedbackResponseViewModel>>(feedbacks);
                return new ApiResponse<List<FeedbackResponseViewModel>>(true, "Feedbacks retrieved successfully", responseViewModels);
            }
            catch (Exception)
            {
                return new ApiResponse<List<FeedbackResponseViewModel>>(false, "An error occurred while retrieving feedback", null!);
            }
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<FeedbackResponseViewModel>> GetFeedbackById(int id)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByUserIdAsync(id);
                var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(feedback);
                return new ApiResponse<FeedbackResponseViewModel>(true, "Feedback retrieved successfully", responseViewModel);
            }
            catch (KeyNotFoundException ex)
            {
                return new ApiResponse<FeedbackResponseViewModel>(false, ex.Message, null);
            }
           
            catch (Exception ex)
            {
                return new ApiResponse<FeedbackResponseViewModel>(false, "An error occurred while retrieving feedback", null);
            }
            
        }
        
        [HttpPost("{id}/respond")]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<ApiResponse<FeedbackResponseViewModel>> RespondToFeedback(int id, [FromBody] FeedbackToResponseViewModel model)
        {
            try
            {
                var responseDto = _mapper.Map<FeedbackResponseDto>(model);
                var updatedFeedback = await _feedbackService.RespondToFeedbackAsync(id, responseDto);
                var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(updatedFeedback);
                return new ApiResponse<FeedbackResponseViewModel>(true, "Response added successfully", responseViewModel);
            }
            catch (KeyNotFoundException ex)
            {
                return new ApiResponse<FeedbackResponseViewModel>(false, ex.Message, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse<FeedbackResponseViewModel>(false, "An error occurred while responding to feedback", null);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ApiResponse<List<FeedbackResponseViewModel>>> GetFeedbackByUserId(int userId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackByUserIdAsync(userId);
                var responseViewModels = _mapper.Map<List<FeedbackResponseViewModel>>(feedbacks);
                return new ApiResponse<List<FeedbackResponseViewModel>>(true, "Feedbacks retrieved successfully", responseViewModels);
            }
            catch (Exception)
            {
                return new ApiResponse<List<FeedbackResponseViewModel>>(false, "An error occurred while retrieving feedback", null);
            }
        }

    }


}
