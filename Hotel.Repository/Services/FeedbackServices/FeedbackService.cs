using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.FeedbackDtos.FeedbackResponseDtos;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Repository.UnitOfWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hotel.Repository.Services.FeedbackServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork ,IMapper  mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<FeedbackDto> AddFeedbackAsync(AddFeedbackDto dto)
        {
            try
            {
                // Check for duplicate feedback
                var existingFeedback =  _unitOfWork.Repository<Feedback>()
                    .GetAllByCriteria(f => f.ReservationId == dto.ReservationId && f.CustomerId == dto.CustomerId);


                if (existingFeedback.Any())
                {
                    throw new InvalidOperationException("Feedback already exists for this reservation.");
                }

                var feedback = _mapper.Map<Feedback>(dto);
                feedback.CreatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Feedback>().AddAsync(feedback);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<FeedbackDto>(feedback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync()
        {
            var feedbacks = await _unitOfWork.Repository<Feedback>().GetAllAsync();
            return _mapper.Map<List<FeedbackDto>>(feedbacks);
        }


        public async Task<List<FeedbackDto>> GetFeedbackByUserIdAsync(int userId)
        {
            var feedbacks =  _unitOfWork.Repository<Feedback>().GetAllByCriteria(f => f.CustomerId == userId);
            if (feedbacks == null)
                throw new KeyNotFoundException($"Customer ID not found.");
            return _mapper.Map<List<FeedbackDto>>(feedbacks);
        }
        public async Task<FeedbackDto> RespondToFeedbackAsync(int id, FeedbackResponseDto responseDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid feedback ID.");
            }

            if (responseDto == null)
            {
                throw new ArgumentNullException(nameof(responseDto), "Response data cannot be null.");
            }

            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {id} not found.");
            }

            // Update only specific properties
             feedback.Response = responseDto.Response;

            await _unitOfWork.Repository<Feedback>().UpdateInclude(feedback, nameof(feedback.Response));
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FeedbackDto>(feedback);
        }

        async Task<FeedbackDto> IFeedbackService.GetFeedbackByIdAsync(int feedbackId)
        {
           var feeback=await _unitOfWork.Repository<Feedback>().GetByIdAsync(feedbackId);
            if (feeback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {feedbackId} not found.");
            }
            return _mapper.Map<FeedbackDto>(feeback);
        }
    }
}
