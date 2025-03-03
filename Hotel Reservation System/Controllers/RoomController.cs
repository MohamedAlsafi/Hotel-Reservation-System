using AutoMapper;
using Hotel.Core.Dtos.Room;
using Hotel.Repository.Services.RoomService;
using Hotel_Reservation_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly IMapper _mapper;

        public RoomController(IRoomServices roomServices, IMapper mapper)
        {
            _roomServices = roomServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<RoomResponseViewModel>>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomServices.GetAvailableRoomsAsync();
                return Ok(_mapper.Map<List<RoomResponseViewModel>>(rooms));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResponseViewModel>> GetRoomById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room ID.");

            try
            {
                var room = await _roomServices.GetRoomByIdAsync(id);
                if (room == null)
                    return NotFound($"Room with ID {id} not found.");

                return Ok(_mapper.Map<RoomResponseViewModel>(room));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponseViewModel>> AddRoom([FromBody] RoomCreateViewModel roomVM)
        {
            if (roomVM == null)
                return BadRequest("Room data is required.");

            try
            {
                var roomDTO = _mapper.Map<RoomDTO>(roomVM);
                var newRoom = await _roomServices.AddRoomAsync(roomDTO);
                return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, _mapper.Map<RoomResponseViewModel>(newRoom));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponseViewModel>> UpdateRoom(int id, [FromBody] RoomUpdateViewModel roomVM)
        {
            if (id <= 0)
                return BadRequest("Invalid room ID.");

            if (roomVM == null)
                return BadRequest("Room data is required.");

            try
            {
                var roomDTO = _mapper.Map<RoomDTO>(roomVM);
                var updatedRoom = await _roomServices.UpdateRoomAsync(id, roomDTO);

                if (updatedRoom == null)
                    return NotFound($"Room with ID {id} not found.");

                return Ok(_mapper.Map<RoomResponseViewModel>(updatedRoom));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room ID.");

            try
            {
                var isDeleted = await _roomServices.DeleteRoomAsync(id);

                if (!isDeleted)
                    return NotFound($"Room with ID {id} not found.");

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }

}