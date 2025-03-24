using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.FeedbackDtos.FeedbackResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.FeedbackServices
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> AddFeedbackAsync(AddFeedbackDto dto);
        Task<IEnumerable<FeedbackDto>> GetAllFeedbackAsync();
        Task<List<FeedbackDto>> GetFeedbackByUserIdAsync(int userId);
        Task<FeedbackDto> GetFeedbackByIdAsync(int feedbackId);

        Task<FeedbackDto> RespondToFeedbackAsync(int id, FeedbackResponseDto responseDto);
    }
}
