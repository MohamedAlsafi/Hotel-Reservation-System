using Hotel.Core.Dtos.Room;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Dtos.Room.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.RoomService
{
    public interface IRoomServices
    {
        Task<CreateRoomResponseDTO> AddRoomAsync(CreateRoomDTO room);
        Task<UpdateRoomBasicResponseDto> UpdateRoomBasicAsync( UpdateRoomBasicDto room);
        Task<UpdateRoomFacilitiesResponseDto> UpdateRoomFacilitiesAsync(UpdateRoomFacilitiesDto facilitiesDto);
        Task<UpdateRoomImagesResponseDto> UpdateRoomImagesAsync(UpdateRoomImagesDto imagesDto);
        Task<bool> DeleteRoomAsync(int id);
        Task<List<RoomResponseDTO>> GetAllRoomsAsync();
        Task<RoomResponseDTO> GetRoomByIdAsync(int id);
        public Task<RoomResponseDTO> SearchForRoomAsync(int roomId);
    }
}
