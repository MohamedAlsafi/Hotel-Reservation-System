using AutoMapper;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Entities.customer;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.HotelStaffs;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.UnitOfWork;
using Hotel.Repository.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task<ReservationViewModel> CreateReservationAsync(CreateReservationDto reservationDto, int customerId)
        {
         
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(reservationDto.RoomId);
            if (room == null)
                return null;

        
            var selectedFacilities = new List<Facility>();
            if (reservationDto.FacilityIds is IEnumerable<int> facilityIds && facilityIds.Any())
            {
                selectedFacilities = await _unitOfWork.Repository<Facility>()
                    .GetAll()
                    .Where(f => facilityIds.Contains(f.Id))
                    .ToListAsync();
            }

      
            var offer = await _unitOfWork.Repository<Offer>()
                .GetAll()
                .FirstOrDefaultAsync(o => o.Id == room.Id && o.StartDate <= reservationDto.CheckInDate && o.EndDate >= reservationDto.CheckOutDate);

            decimal roomPrice = room.Price;
            if (offer != null)
            {
          
                roomPrice -= (roomPrice * offer.DiscountPercentage / 100);
            }

      
            decimal facilitiesTotal = selectedFacilities.Sum(f => f.Price);

    
            decimal totalPrice = roomPrice + facilitiesTotal;

            var reservation = new Reservation
            {
                CustomerId = customerId,
                RoomId = room.Id,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate,
                Facilities = selectedFacilities,
                TotalPrice = totalPrice
            };

            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            var reservationVm = _mapper.Map<ReservationViewModel>(reservation);
            reservationVm.TotalPrice = totalPrice;

            return reservationVm;
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

