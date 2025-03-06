using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;
using Hotel.Core.Helpers;

namespace Hotel.Core.Profiles
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
           CreateMap<RoomImage, RoomDTO>().ForMember(R => R.ImageUrl, options => options.MapFrom<PictureUrlResolver>());
            CreateMap<RoomDTO, Room>()
             .ForMember(dest => dest.RoomFacilities, opt => opt.Ignore())
             .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Room, RoomResponseDTO>()
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
             .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.RoomFacilities.Select(rf => rf.Facility.Name)))
             .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));

            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<Offer, CreateOfferDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
