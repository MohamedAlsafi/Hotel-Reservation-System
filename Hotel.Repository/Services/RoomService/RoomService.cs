﻿using AutoMapper;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.RoomService
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
            
            var room = _mapper.Map<Room>(roomDTO);

            await _unitOfWork.Repository<Room>().AddAsync(room);
            await _unitOfWork.CompleteAsync(); 

            if (roomDTO.FacilityIds != null && roomDTO.FacilityIds.Any())
            {
                var validFacilityIds = await _unitOfWork.Repository<Facility>()
                    .GetAllByCriteria(f => roomDTO.FacilityIds.Contains(f.Id))
                    .Select(f => f.Id)
                    .ToListAsync();

                var roomFacilities = validFacilityIds
                    .Select(fid => new RoomFacility { RoomId = room.Id, FacilityId = fid })
                    .ToList();

                 await _unitOfWork.Repository<RoomFacility>().AddRangeAsync(roomFacilities);
               

            }

            if (!string.IsNullOrEmpty(roomDTO.ImageUrl))
            {
                var roomImages = new List<RoomImage> { new RoomImage { RoomId = room.Id, ImageUrl = roomDTO.ImageUrl } };
                await _unitOfWork.Repository<RoomImage>().AddRangeAsync(roomImages);
            }

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoomResponseDTO>(room);
        }

        public async Task<RoomResponseDTO> GetRoomByIdAsync(int id)
        {
            var room = await GetRoomEntityByIdAsync(id);
            if (room is  null)
                return null;

            return _mapper.Map<RoomResponseDTO>(room);
        }
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await GetRoomEntityByIdAsync(id); 
            if (room == null) return false;

            _unitOfWork.Repository<Room>().SoftDelete(room);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<RoomResponseDTO>> GetAvailableRoomsAsync()
        {
            var rooms = await _unitOfWork.Repository<Room>()
            .GetAllByCriteria(r => r.IsAvailable).ToListAsync();
            return _mapper.Map<List<RoomResponseDTO>>(rooms);
        }


        public async Task<RoomResponseDTO> UpdateRoomAsync(int id, RoomDTO roomDTO)
        {
            var room = await GetRoomEntityByIdAsync(id);
            if (room == null) return null;

            _mapper.Map(roomDTO, room);

            _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.RoomNumber), nameof(Room.Type), nameof(Room.Price), nameof(Room.IsAvailable));

            if (roomDTO.FacilityIds != null)
            {
                room.RoomFacilities = roomDTO.FacilityIds.Select(fid => new RoomFacility { RoomId = id, FacilityId = fid }).ToList();
                _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.RoomFacilities));
            }

           
            if (!string.IsNullOrEmpty(roomDTO.ImageUrl))
            {
                room.Images = new List<RoomImage> { new RoomImage { RoomId = id, ImageUrl = roomDTO.ImageUrl } };
                _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.Images));
            }

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RoomResponseDTO>(room);
        }


        private async Task<Room> GetRoomEntityByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Room>().GetByIdAsync(id);
        }

        
    }
}
