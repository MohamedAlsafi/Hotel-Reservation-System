using Hotel.Repository.Dtos.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Room
{
    public interface IRoomServices
    {
        Task<RoomResponseDTO> AddRoomAsync(RoomDTO room);
        Task<RoomResponseDTO> UpdateRoomAsync(int id, RoomDTO room);
        Task<bool> DeleteRoomAsync(int id);
        Task<List<RoomResponseDTO>> GetAvailableRoomsAsync();
        Task<RoomResponseDTO> GetRoomByIdAsync(int id);
    }
}
