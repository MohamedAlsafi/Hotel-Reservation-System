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
        private readonly HotelDbContext _dbContext;


        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbContext = dbContext;

        }

        public async Task<ApiResponse<ReservationViewModel>> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
                return new ApiResponse<ReservationViewModel>(false, "Start date must be before end date", null);

            if (!await _dbContext.Customers.AnyAsync(c => c.Id == reservationDto.CustomerId))
                return new ApiResponse<ReservationViewModel>(false, "Customer does not exist", null);

            if (!await _dbContext.Rooms.AnyAsync(r => r.Id == reservationDto.RoomId))
                return new ApiResponse<ReservationViewModel>(false, "Room does not exist", null);

            var isRoomAvailable = !await _dbContext.Reservations.AnyAsync(r => r.RoomId == reservationDto.RoomId &&
                (r.CheckInDate < reservationDto.CheckOutDate && r.CheckOutDate > reservationDto.CheckInDate));

            if (!isRoomAvailable)
                return new ApiResponse<ReservationViewModel>(false, "Room is already booked for the selected dates", null);

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
