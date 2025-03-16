using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Dtos.Room.Update;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Rooms;
using Hotel_Reservation_System.ViewModels;
using Hotel_Reservation_System.ViewModels.Feedback;
using Hotel_Reservation_System.ViewModels.Room;
using RoomResponseViewModel = Hotel_Reservation_System.ViewModels.Room.RoomResponseViewModel;

namespace Hotel_Reservation_System.ProfilesVM
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            //  map `DTOs` و `ViewModels`
            CreateMap<RoomResponseDTO, RoomResponseViewModel>();
            CreateMap<ViewModels.Room.RoomCreateViewModel, CreateRoomDTO>();
            CreateMap<CreateRoomResponseDTO, RoomResponseViewModel>();

            // تحويل من ViewModel إلى DTO
            CreateMap<RoomBasicInfoResponseViewModel, UpdateRoomBasicDto>();
            CreateMap<UpdateRoomBasicResponseDto, RoomBasicInfoResponseViewModel>();

            CreateMap<UpdateRoomFacilitiesViewModel, UpdateRoomFacilitiesDto>();
            CreateMap<UpdateRoomFacilitiesResponseDto, RoomFacilitiesResponseViewModel>();

            CreateMap<UpdateRoomImagesViewModel, UpdateRoomImagesDto>();
            CreateMap<UpdateRoomImagesResponseDto, RoomImagesResponseViewModel>();

            CreateMap<UpdateRoomDetailsViewModel, UpdateRoomBasicDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
           . ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ReverseMap();

            #region Feedback

            CreateMap<AddFeedbackViewModel, AddFeedbackDto>().ReverseMap();
            CreateMap<AddFeedbackViewModel, FeedbackResponseViewModel>().ReverseMap();
            CreateMap<FeedbackDto, FeedbackResponseViewModel>().ReverseMap();
            #endregion

        }
    }
}
