using AutoMapper;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Repository.ViewModels;
using Hotel_Reservation_System.ViewModels;

namespace Hotel_Reservation_System.ProfilesVM
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            //  map `DTOs` و `ViewModels`
            CreateMap<RoomResponseDTO, RoomResponseViewModel>();
            CreateMap<RoomCreateViewModel, RoomDTO>();
            CreateMap<RoomUpdateViewModel, RoomDTO>();
            CreateMap<Offer, OfferViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();

            CreateMap<ReservationViewModel, ReservationDto>().ReverseMap();

        }
    }
}
