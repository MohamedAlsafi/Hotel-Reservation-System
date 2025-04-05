using AutoMapper;
using Hotel.Core.Dtos.FeedbackDtos;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Dtos.Reservation;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Dtos.Room.Update;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.FeedbackModel;
using Hotel.Core.Entities.OfferModel;
using Hotel.Core.Entities.Reservation;
using Hotel.Core.Entities.Rooms;

namespace Hotel.Core.Profiles
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<CreateRoomDTO, Room>()
                .ForMember(dest => dest.RoomFacilities, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Room, CreateRoomResponseDTO>()
                .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src =>
                    src.RoomFacilities.Select(rf => rf.Facility.Name).ToList()))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.Images.Select(img => $"/uploads/{img.ImageUrl}").ToList()));

            CreateMap<UpdateRoomBasicDto, Room>()
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<RoomType>(src.Type)));

            CreateMap<Room, UpdateRoomBasicResponseDto>();

            CreateMap<UpdateRoomFacilitiesDto, Room>()
                .ForMember(dest => dest.RoomFacilities, opt => opt.Ignore());

            CreateMap<Room, UpdateRoomFacilitiesResponseDto>()
            .ForMember(dest => dest.FacilityIds, opt => opt.MapFrom(src => src.RoomFacilities.Select(f => f.FacilityId).ToList()));

            CreateMap<UpdateRoomImagesDto, Room>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Room, UpdateRoomImagesResponseDto>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));


            CreateMap<Room, RoomResponseDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.RoomFacilities.Select(rf => rf.Facility.Name)))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));

            // Offer to DTO (includes mapping CreatedByStaff.Id)
            #region Offer
            CreateMap<Offer, OfferDto>().ReverseMap();
            // In your AutoMapper Profile (e.g., OfferProfile.cs)
            CreateMap<Offer, OfferListingDto>()
                .ForMember(dest => dest.ValidUntil, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()));
            #endregion

            // CreateOfferDto to Offer (ignore CreatedByStaff completely)
            CreateMap<CreateOfferDto, Offer>()
                .ForMember(dest => dest.CreatedByStaff, opt => opt.Ignore()) // Explicitly ignore
                .ReverseMap()
                .ForMember(dest => dest.CreatedByStaff, opt => opt.MapFrom(src => src.CreatedByStaff != null ? src.CreatedByStaff.Id : (int?)null));
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Feedback , FeedbackDto>().ReverseMap();
            


            //Mapping Feedback
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, AddFeedbackDto>().ReverseMap();
            CreateMap<AddFeedbackDto, FeedbackDto>().ReverseMap();
            CreateMap<FeedbackResponseDto, FeedbackDto>().ReverseMap();
            CreateMap<FeedbackResponseDto, Feedback>().ReverseMap();

        }
    }
}
