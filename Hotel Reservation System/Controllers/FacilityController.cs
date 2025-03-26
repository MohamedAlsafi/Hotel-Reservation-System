using AutoMapper;
using Hotel.Core.Dtos.Facility;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Roles = "Staff")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddFacility([FromBody] FacilityDTO facilityDTO)
        {
            if (string.IsNullOrEmpty(facilityDTO.Name))
                return BadRequest("Facility name is required");

            var facilityRepo = _unitOfWork.Repository<Facility>();

            var existingFacility = await facilityRepo.GetByCriteriaAsync(f => f.Name == facilityDTO.Name);
            if (existingFacility != null)
                return BadRequest("Facility already exists");

            var facility = new Facility { Name = facilityDTO.Name };
            await facilityRepo.AddAsync(facility);
            await _unitOfWork.CompleteAsync();

            return Ok(facility);
        }

        [Authorize(Roles = "Staff")]

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllFacilities()
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facilities = facilityRepo.GetAll().ToList();
            return Ok(facilities);
        }

        [Authorize(Roles = "Staff")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacilityById(int id)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

            return Ok(facility);
        }
        [Authorize(Roles = "Staff")]

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateFacility(int id, [FromBody] FacilityDTO facilityDTO)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

            facility.Name = facilityDTO.Name;
            facilityRepo.UpdateInclude(facility, nameof(Facility.Name));
            await _unitOfWork.CompleteAsync();

            return Ok(facility);
        }
        [Authorize(Roles = "Staff")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

            facilityRepo.SoftDelete(facility);
            await _unitOfWork.CompleteAsync();

            return Ok("Facility deleted successfully");
        }
    }
}
