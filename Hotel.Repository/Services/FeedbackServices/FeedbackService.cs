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
                var feedback = _mapper.Map<Feedback>(dto);

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


    }
}
