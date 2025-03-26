using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.UnitOfWork;
using Hotel.Repository.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _dbContext;

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

            var reservationVm = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation created successfully", reservationVm);
        }


        public async Task<ResponseViewModel<bool>> CancelReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<bool>(false, "Reservation not found", false);

            _unitOfWork.Repository<Reservation>().HardDelete(reservation);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseViewModel<bool>(true, "Reservation canceled successfully", true);
        }


        public async Task<ResponseViewModel<IEnumerable<ReservationViewModel>>> GetAllReservationsAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>().GetAll().ToListAsync();
            var reservationVms = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);
            return new ResponseViewModel<IEnumerable<ReservationViewModel>>(true, "Reservations retrieved successfully", reservationVms);
        }

        public async Task<ResponseViewModel<ReservationViewModel>> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null);

            var reservationVm = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation retrieved successfully", reservationVm);
        }

        public async Task<ResponseViewModel<bool>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<bool>(false, "Reservation not found", false);

            _mapper.Map(reservationDto, reservation);
            _unitOfWork.Repository<Reservation>().UpdateExclude(reservation);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseViewModel<bool>(true, "Reservation updated successfully", true);
        }

        public async Task<ResponseViewModel<bool>> ProvideFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            await _unitOfWork.Repository<Feedback>().AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseViewModel<bool>(true, "Feedback submitted successfully", true);
        }

    }
}

