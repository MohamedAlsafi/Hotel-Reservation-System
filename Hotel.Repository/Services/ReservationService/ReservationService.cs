using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.IGenericRepository;
using Hotel.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IGenericRepository<Reservation> reservationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return reservation == null ? null : _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) return false;

            _mapper.Map(reservationDto, reservation);
            _reservationRepository.UpdateInclude(reservation, nameof(Reservation.CheckInDate), nameof(Reservation.CheckOutDate));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) return false;

            _reservationRepository.SoftDelete(reservation);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            await _reservationRepository.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
            return reservation;
        }
        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) return false;

            _reservationRepository.SoftDelete(reservation);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
