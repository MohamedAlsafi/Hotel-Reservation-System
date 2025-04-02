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

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseViewModel<ReservationViewModel>> CreateReservationAsync(CreateReservationDto reservation , int CustomerId)
        {
            var mappedReservation = _mapper.Map<Reservation>(reservation);
            mappedReservation.CustomerId = CustomerId;

            await _unitOfWork.Repository<Reservation>().AddAsync(mappedReservation);
            await _unitOfWork.SaveChangesAsync();

            var reservationVm = _mapper.Map<ReservationViewModel>(mappedReservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation created successfully", reservationVm);
        }

        public async Task<ResponseViewModel<ReservationViewModel>> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null);

            var reservationVm = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation retrieved successfully", reservationVm);
        }

        public async Task<ResponseViewModel<ReservationViewModel>> UpdateReservationAsync(UpdateReservationDto reservationDto)
        {
           if(reservationDto is null)
                return new ResponseViewModel<ReservationViewModel>(false, "", null);

            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationDto.Id);
            if (reservation is null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null);
            var mappedReservation = _mapper.Map(reservationDto, reservation);

            _unitOfWork.Repository<Reservation>().UpdateInclude(mappedReservation);

            return new ResponseViewModel<ReservationViewModel>(true, "Reservation updated successfully", null);
        }

        public async Task<ResponseViewModel<ReservationViewModel>> CancelReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                return new ResponseViewModel<ReservationViewModel>(false, "Reservation not found", null);

            _unitOfWork.Repository<Reservation>().HardDelete(reservation);
            await _unitOfWork.SaveChangesAsync();

            var reservationVm = _mapper.Map<ReservationViewModel>(reservation);
            return new ResponseViewModel<ReservationViewModel>(true, "Reservation canceled successfully", reservationVm);
        }

       
    }
}

