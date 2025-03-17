using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.FeedbackDtos.FeedbackResponseDtos;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if (existingFeedback != null && existingFeedback.Any())
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

        public async Task<List<FeedbackDto>> GetAllFeedbackAsync()
        {
            var feedbacks = await _unitOfWork.Repository<Feedback>().GetAllAsync();
            return _mapper.Map<List<FeedbackDto>>(feedbacks);
        }

        public async Task<FeedbackDto> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {id} not found.");
            }

            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<List<FeedbackDto>> GetFeedbackByUserIdAsync(int userId)
        {
            var feedbacks =  _unitOfWork.Repository<Feedback>().GetAllByCriteria(f => f.CustomerId == userId);
            return _mapper.Map<List<FeedbackDto>>(feedbacks);
        }
        public async Task<FeedbackDto> RespondToFeedbackAsync(int id, FeedbackResponseDto responseDto)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {id} not found.");
            }

            feedback.Response = responseDto.Response;
         

            _unitOfWork.Repository<Feedback>().UpdateInclude(feedback);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FeedbackDto>(feedback);
        }
    }
}
