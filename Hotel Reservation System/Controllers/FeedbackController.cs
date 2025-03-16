using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Repository.Services.FeedbackServices;
using Hotel_Reservation_System.ViewModels.Feedback;
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

        public async Task<ApiResponse<FeedbackResponseViewModel>> AddFeeback(AddFeedbackViewModel model)
        {
            var feedback = _mapper.Map<AddFeedbackDto>(model);
            var resultDto = await _feedbackService.AddFeedbackAsync(feedback);

            // Map the plain DTO to your response view model
            var responseViewModel = _mapper.Map<FeedbackResponseViewModel>(resultDto);

            // Create a successful API response with the mapped view model
            return new ApiResponse<FeedbackResponseViewModel>(true, "Feedback submitted successfully", responseViewModel);
        }
    }


}
