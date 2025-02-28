using AutoMapper;
using Hotel.Core.Entities.Room;
using Hotel.Core.Helpers;
using Hotel.Repository.Dtos.Room;

namespace Hotel_Reservation_System.MappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomImage , RoomDTO>().ForMember(R=>R.ImageUrl , options=>options.MapFrom<PictureUrlResolver>());
        }
    }
}
