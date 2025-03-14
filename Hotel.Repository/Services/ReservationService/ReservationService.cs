using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.Reservation;
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
        private readonly CustomerIdentityDbContext _customerIdentityDbContext;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper, CustomerIdentityDbContext customerIdentityDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerIdentityDbContext = customerIdentityDbContext;
        }


        public async Task<ApiResponse<ReservationViewModel>> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
            var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);
            return new ApiResponse<ReservationViewModel>(true, "Reservation created successfully", reservationViewModel);
        }

        public async Task<ApiResponse<bool>> CancelReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ApiResponse<bool>(false, "Reservation not found", false);

            _unitOfWork.Repository<Reservation>().SoftDelete(reservation);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Reservation cancelled successfully", true);
        }

        public async Task<ApiResponse<IEnumerable<ReservationViewModel>>> GetAllReservationsAsync(int id)
        {
            var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsync();
            var reservation = reservations.FirstOrDefault(r => r.Id == id );


            var reservationViewModels = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);
            return new ApiResponse<IEnumerable<ReservationViewModel>>(true, "Reservations retrieved successfully", reservationViewModels);
        }

        public async Task<ApiResponse<ReservationViewModel>> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ApiResponse<ReservationViewModel>(false, "Reservation not found", null);

            var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);
            return new ApiResponse<ReservationViewModel>(true, "Reservation retrieved successfully", reservationViewModel);
        }

        public async Task<ApiResponse<bool>> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ApiResponse<bool>(false, "Reservation not found", false);

            _mapper.Map(reservationDto, reservation);
            _unitOfWork.Repository<Reservation>().UpdateInclude(reservation);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Reservation updated successfully", true);
        }
        public async Task<ApiResponse<IEnumerable<ReservationViewModel>>> GetAllReservationsAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsync();
            var reservationViewModels = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);

            return new ApiResponse<IEnumerable<ReservationViewModel>>(true, "Reservations retrieved successfully", reservationViewModels);
        }
    }
}
