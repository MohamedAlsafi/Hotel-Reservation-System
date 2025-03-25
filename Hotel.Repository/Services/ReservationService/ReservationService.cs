using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.UnitOfWork;
using Hotel.Repository.ViewModels;

namespace Hotel.Repository.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseViewModel<ReservationViewModel>> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
            var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation created successfully", reservationViewModel);
        }

        public async Task<ResponseViewModel<ReservationViewModel>> CancelReservationAsync(int id)
        {
            if(id <= 0)
                return new ResponseViewModel<ReservationViewModel>(false, "Invalid reservation id", null!);
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation is null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null!);

            _unitOfWork.Repository<Reservation>().SoftDelete(reservation);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation cancelled successfully", null!);
        }


        public async Task<ResponseViewModel<ReservationViewModel>> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation is null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null);

            var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation retrieved successfully", reservationViewModel);
        }
        public async Task<ResponseViewModel<bool>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<bool>(false, "Reservation not found", false);
           await _unitOfWork.Repository<Reservation>().UpdateInclude(reservation , nameof(Reservation.RoomId) , nameof(Reservation.TotalPrice) , nameof(Reservation.PaymentStatus));
            await _unitOfWork.SaveChangesAsync();
            return new ResponseViewModel<bool>(true, "Reservation updated successfully", true);
        }


        public async Task<ResponseViewModel<bool>> ProvideFeedbackAsync(FeedbackDto mappedFeedback)
        {
            var feedback = _mapper.Map<Feedback>(mappedFeedback);
            await _unitOfWork.Repository<Feedback>().AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseViewModel<bool>(true, "Feedback provided successfully", true);
        }
    }
}
