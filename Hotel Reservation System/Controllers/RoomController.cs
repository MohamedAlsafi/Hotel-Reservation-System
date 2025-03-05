using AutoMapper;
using Hotel.Core.Dtos.Room;
using Hotel.Repository.Services.RoomService;
using Hotel_Reservation_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly IMapper _mapper;

        public RoomController( IRoomServices roomServices , IMapper mapper)
        {
            _roomServices = roomServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<RoomResponseViewModel>> GetAllRooms()
        {
            var rooms = await _roomServices.GetAvailableRoomsAsync();
            return _mapper.Map<List<RoomResponseViewModel>>(rooms);
        }

        [HttpGet("{id}")]
        public async Task<RoomResponseViewModel> GetRoomById(int id)
        {
            var room = await _roomServices.GetRoomByIdAsync(id);
            return _mapper.Map<RoomResponseViewModel>(room);
        }

        [HttpPost]
        public async Task<RoomResponseViewModel> AddRoom([FromBody] RoomCreateViewModel roomVM)
        {
            var roomDTO = _mapper.Map<RoomDTO>(roomVM);
            var newRoom = await _roomServices.AddRoomAsync(roomDTO);
            return _mapper.Map<RoomResponseViewModel>(newRoom);
        }

        
        [HttpPut("{id}")]
        public async Task<RoomResponseViewModel> UpdateRoom(int id, [FromBody] RoomUpdateViewModel roomVM)
        {
            var roomDTO = _mapper.Map<RoomDTO>(roomVM);
            var updatedRoom = await _roomServices.UpdateRoomAsync(id, roomDTO);
            return _mapper.Map<RoomResponseViewModel>(updatedRoom);
        }

        
        [HttpDelete("{id}")]
        public async Task<bool> DeleteRoom(int id)
        {
            return await _roomServices.DeleteRoomAsync(id);
        }

    }
}
