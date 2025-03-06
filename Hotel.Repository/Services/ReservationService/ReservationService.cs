using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReservationService( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>().GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            return reservation == null ? null : _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null) return false;

            _mapper.Map(reservationDto, reservation);
            _unitOfWork.Repository<Reservation>().UpdateInclude(reservation, nameof(Reservation.CheckInDate), nameof(Reservation.CheckOutDate));
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null) return false;

            _unitOfWork.Repository<Reservation>().SoftDelete(reservation);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return reservation;
        }
        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null) return false;

            _unitOfWork.Repository<Reservation>().SoftDelete(reservation);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        //public Task<ReservationDto> GetReservationWithCustomerAsync(CustomerViewModel customer)
        //{
        //    _unitOfWork.Repository<Reservation>().GetAllByCriteria(c => c.Customer.NormalizedUserName );
        //}
    }
}
