using AutoMapper;
using Hotel.Core.Dtos.Room;
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
        }
    }
}
