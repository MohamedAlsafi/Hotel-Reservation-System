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
        public Task<FeedbackDto> AddFeedbackAsync(AddFeedbackDto dto);

    }
}
