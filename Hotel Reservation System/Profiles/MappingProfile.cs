using AutoMapper;
using Hotel.Core.Entities.Rooms;
using Hotel.Core.Helpers;
using Hotel.Repository.Dtos.Room;

namespace Hotel_Reservation_System.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
             CreateMap<RoomImage , RoomDTO>().ForMember(R=>R.ImageUrl , options=>options.MapFrom<PictureUrlResolver>());
            CreateMap<RoomDTO, Room>()
             .ForMember(dest => dest.RoomFacilities, opt => opt.Ignore())
             .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Room, RoomResponseDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.RoomFacilities.Select(rf => rf.Facility.Name)))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));
        }
    }
}
