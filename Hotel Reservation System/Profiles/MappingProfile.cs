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
        }
    }
}
