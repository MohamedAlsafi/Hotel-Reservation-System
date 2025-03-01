using AutoMapper;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.Dtos.Room;
using Hotel.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Room
{
    public class RoomService : IRoomServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoomResponseDTO> AddRoomAsync(RoomDTO roomDTO)
        {
            //var room = _mapper.Map<Room>(roomDTO);


            //if (roomDTO.FacilityIds != null && roomDTO.FacilityIds.Any())
            //{
            //    room.RoomFacilities = roomDTO.FacilityIds
            //        .Select(fid => new RoomFacility { FacilityId = fid })
            //        .ToList();
            //}


            //if (!string.IsNullOrEmpty(roomDTO.ImageUrl))
            //{
            //    room.Images = new List<RoomImage> { new RoomImage { ImageUrl = roomDTO.ImageUrl } };
            //}

            //await _unitOfWork.Repository<Room>().AddAsync(room);
            //await _unitOfWork.CompleteAsync();

            //return _mapper.Map<RoomResponseDTO>(room);
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRoomAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoomResponseDTO>> GetAvailableRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomResponseDTO> GetRoomByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RoomResponseDTO> UpdateRoomAsync(int id, RoomDTO room)
        {
            throw new NotImplementedException();
        }
    }
}
