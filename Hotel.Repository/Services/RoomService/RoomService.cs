using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Hotel.Core.Dtos.Room;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Dtos.Room.Update;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IValidator<CreateRoomDTO> _roomValidator;

        public RoomService(IUnitOfWork unitOfWork , IMapper mapper, IValidator<CreateRoomDTO> roomValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roomValidator = roomValidator;
        }
        public async Task<CreateRoomResponseDTO> AddRoomAsync(CreateRoomDTO roomDTO)
        {
            var validationResult = await _roomValidator.ValidateAsync(roomDTO);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Validation failed: {errors}"); 
            }
            bool isRoomExists = await _unitOfWork.Repository<Room>()
            .GetAllByCriteria(r => r.RoomNumber == roomDTO.RoomNumber)
            .AnyAsync();

            if (isRoomExists)
                throw new Exception("This room number is already in use.");

              var room = _mapper.Map<Room>(roomDTO);
           
            await _unitOfWork.Repository<Room>().AddAsync(room);
            await _unitOfWork.CompleteAsync();

            await AddRoomFacilitiesAsync(room.Id, roomDTO.FacilityIds);

            await AddRoomImagesAsync(room.Id, roomDTO.Images);

            return await SelectRoomDto<CreateRoomResponseDTO>(
            _unitOfWork.Repository<Room>().GetAll().Where(r => r.Id == room.Id))
                 .FirstOrDefaultAsync();
        }


        public async Task<RoomResponseDTO?> GetRoomByIdAsync(int id)
        {
            return await SelectRoomDto<RoomResponseDTO>(
                _unitOfWork.Repository<Room>().GetAll().Where(r => r.Id == id)
            ).FirstOrDefaultAsync();
        }

        public async Task<List<RoomResponseDTO>> GetAllRoomsAsync()
        {
            return await SelectRoomDto<RoomResponseDTO>(
                _unitOfWork.Repository<Room>().GetAll()
            ).ToListAsync();
        }


        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await GetRoomEntityByIdAsync(id);
            if (room == null) return false;

            _unitOfWork.Repository<Room>().SoftDelete(room);
            await _unitOfWork.CompleteAsync();
            return true;
        }


        public async Task<UpdateRoomBasicResponseDto> UpdateRoomBasicAsync( UpdateRoomBasicDto roomDTO)
        {
            var room = await GetRoomEntityByIdAsync(roomDTO.Id);
            if (room is null) return null;

            _mapper.Map(roomDTO, room);

            _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.RoomNumber), nameof(Room.Type), nameof(Room.Price));
            await _unitOfWork.CompleteAsync();

           
            await _unitOfWork.CompleteAsync();
           
                return _mapper.Map<UpdateRoomBasicResponseDto>(room);
        }

        public async Task<UpdateRoomFacilitiesResponseDto> UpdateRoomFacilitiesAsync(UpdateRoomFacilitiesDto facilitiesDto)
        {
            var room = await GetRoomEntityByIdAsync(facilitiesDto.RoomId);
            if (room is null) return null;

            var existingFacilities = room.RoomFacilities.Select(f => f.FacilityId).ToList();
            var newFacilities = facilitiesDto.FacilityIds.Except(existingFacilities).ToList();
            var removedFacilities = existingFacilities.Except(facilitiesDto.FacilityIds).ToList();

            var facilitiesToRemove = room.RoomFacilities.Where(f => removedFacilities.Contains(f.FacilityId)).ToList();
            foreach (var facility in facilitiesToRemove)
            {
                room.RoomFacilities.Remove(facility);
            }

            foreach (var fid in newFacilities)
            {
                room.RoomFacilities.Add(new RoomFacility { RoomId = room.Id, FacilityId = fid });
            }

            _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.RoomFacilities));
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateRoomFacilitiesResponseDto>(room);
        }

        public async Task<UpdateRoomImagesResponseDto> UpdateRoomImagesAsync(UpdateRoomImagesDto imagesDto)
        {
            var room = await GetRoomEntityByIdAsync(imagesDto.RoomId);
            if (room is null) return null;

            var newImageUrls = new List<string>();

            if (imagesDto.Images != null && imagesDto.Images.Count > 0)
            {
                foreach (var image in imagesDto.Images)
                {
                    var imageUrl = await SaveImageAsync(image);
                    if (imageUrl != null)
                        newImageUrls.Add(imageUrl);
                }
            }

            var currentImages = room.Images.Select(img => img.ImageUrl).ToList();
            var imagesToKeep = imagesDto.ExistingImageUrls.Intersect(currentImages).ToList();
            var imagesToRemove = currentImages.Except(imagesToKeep).ToList();

            var removedImages = room.Images.Where(img => imagesToRemove.Contains(img.ImageUrl)).ToList();
            foreach (var img in removedImages)
            {
                room.Images.Remove(img);
            }

            foreach (var url in newImageUrls)
            {
                room.Images.Add(new RoomImage { RoomId = room.Id, ImageUrl = url });
            }

            _unitOfWork.Repository<Room>().UpdateInclude(room, nameof(Room.Images));
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UpdateRoomImagesResponseDto>(room);
        }


        private async Task<Room> GetRoomEntityByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Room>().GetByCriteriaAsync(r => r.Id == id);
        }


        private IQueryable<T> SelectRoomDto<T>(IQueryable<Room> query)
        {
            return query.ProjectTo<T>(_mapper.ConfigurationProvider);
        }   

        private async Task AddRoomFacilitiesAsync(int roomId, List<int> facilityIds)
        {
            if (facilityIds is null || !facilityIds.Any()) return;

           
            var validFacilityIds = await _unitOfWork.Repository<Facility>()
                .GetAllByCriteria(f => facilityIds.Contains(f.Id))
                .Select(f => f.Id)
                .ToListAsync();

            var roomFacilities = validFacilityIds
                .Select(fid => new RoomFacility { RoomId = roomId, FacilityId = fid })
                .ToList();

            await _unitOfWork.Repository<RoomFacility>().AddRangeAsync(roomFacilities);
            await _unitOfWork.CompleteAsync();
        }
        private async Task AddRoomImagesAsync(int roomId, List<IFormFile> images)
        {
            if (images is null || !images.Any()) return;

            var roomImages = new List<RoomImage>();

            foreach (var image in images)
            {
                var fileName = await SaveImageAsync(image);
                if (fileName != null)
                {
                    roomImages.Add(new RoomImage { RoomId = roomId, ImageUrl = fileName });
                }
            }

            await _unitOfWork.Repository<RoomImage>().AddRangeAsync(roomImages);
            await _unitOfWork.CompleteAsync();
        }

        
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
                return null;

            
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

      
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            
            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }

        public async Task<RoomResponseDTO> SearchForRoomAsync(int roomId)
        {
            if (roomId <= 0) return null!;
            return await SelectRoomDto<RoomResponseDTO>(
                _unitOfWork.Repository<Room>().GetAll().Where(r => r.Id == roomId)
            ).FirstOrDefaultAsync();
        }
    }
}
