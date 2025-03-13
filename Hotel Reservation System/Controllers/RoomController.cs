using AutoMapper;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Dtos.Room.Update;
using Hotel.Repository.Services.RoomService;
using Hotel_Reservation_System.ViewModels;
using Hotel_Reservation_System.ViewModels.Room;
using Microsoft.AspNetCore.Mvc;
using RoomCreateViewModel = Hotel_Reservation_System.ViewModels.Room.RoomCreateViewModel;
using RoomResponseViewModel = Hotel_Reservation_System.ViewModels.Room.RoomResponseViewModel;

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
            
                var rooms = await _roomServices.GetAllRoomsAsync();
                return Ok(_mapper.Map<List<RoomResponseViewModel>>(rooms));
            
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResponseViewModel>> GetRoomById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room ID.");

            
                var room = await _roomServices.GetRoomByIdAsync(id);
                if (room == null)
                    return NotFound($"Room with ID {id} not found.");

                return Ok(_mapper.Map<RoomResponseViewModel>(room));
            
            
        }

        [HttpPost]
        public async Task<ActionResult<RoomResponseViewModel>> AddRoom([FromForm] RoomCreateViewModel roomVM)
        {
            if (roomVM is null)
                return BadRequest("Room data is required.");

            
                var roomDTO = _mapper.Map<CreateRoomDTO>(roomVM);
                var newRoom = await _roomServices.AddRoomAsync(roomDTO);
                return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, _mapper.Map<RoomResponseViewModel>(newRoom));
            
            
        }

        [HttpPut("basic-info")]
        public async Task<ActionResult<RoomBasicInfoResponseViewModel>> UpdateRoomBasicInfo([FromForm] UpdateRoomDetailsViewModel roomVM)
        {
            if (roomVM.Id <= 0)
                return BadRequest("Invalid room ID.");

            var roomDTO = _mapper.Map<UpdateRoomBasicDto>(roomVM);
            var updatedRoom = await _roomServices.UpdateRoomBasicAsync(roomDTO);

            if (updatedRoom == null)
                return NotFound($"Room with ID {roomVM.Id} not found.");

            return Ok(_mapper.Map<RoomBasicInfoResponseViewModel>(updatedRoom));
        }


        [HttpPut("facilities")]
        public async Task<ActionResult<RoomFacilitiesResponseViewModel>> UpdateRoomFacilities([FromForm] UpdateRoomFacilitiesViewModel roomVM)
        {
            if (roomVM.RoomId <= 0)
                return BadRequest("Invalid room ID.");

            var roomDTO = _mapper.Map<UpdateRoomFacilitiesDto>(roomVM);
            var updatedRoom = await _roomServices.UpdateRoomFacilitiesAsync(roomDTO);

            if (updatedRoom == null)
                return NotFound($"Room with ID {roomVM.RoomId} not found.");

            return Ok(_mapper.Map<RoomFacilitiesResponseViewModel>(updatedRoom));
        }

        [HttpPut("images")]
        public async Task<ActionResult<RoomImagesResponseViewModel>> UpdateRoomImages([FromForm] UpdateRoomImagesViewModel roomVM)
        {
            if (roomVM.RoomId <= 0)
                return BadRequest("Invalid room ID.");

            var roomDTO = _mapper.Map<UpdateRoomImagesDto>(roomVM);
            var updatedRoom = await _roomServices.UpdateRoomImagesAsync(roomDTO);

            if (updatedRoom == null)
                return NotFound($"Room with ID {roomVM.RoomId} not found.");

            return Ok(_mapper.Map<RoomImagesResponseViewModel>(updatedRoom));
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room ID.");

                var isDeleted = await _roomServices.DeleteRoomAsync(id);

                if (!isDeleted)
                    return NotFound($"Room with ID {id} not found.");

                return Ok(true);
            
            
        }
    }

}