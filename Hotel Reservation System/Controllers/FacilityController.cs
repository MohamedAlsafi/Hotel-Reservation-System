using AutoMapper;
using Hotel.Core.Dtos.Facility;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities.Rooms;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Filter;
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

        public FacilityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.AddFacility })]
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

        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetAllFacilities })]

        [HttpGet("GetAll")]
        public IActionResult GetAllFacilities()
        {
            var facilities = _unitOfWork.Repository<Facility>().GetAll().ToList();
            return Ok(facilities);
        }

        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.GetFacilityById })]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacilityById(int id)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

            return Ok(facility);
        }
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.UpdateFacility })]

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateFacility(int id, [FromBody] FacilityDTO facilityDTO)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

            facility.Name = facilityDTO.Name;
          await  facilityRepo.UpdateInclude(facility, nameof(Facility.Name));
            await _unitOfWork.CompleteAsync();

            return Ok(facility);
        }
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { Features.DeleteFacility })]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facilityRepo = _unitOfWork.Repository<Facility>();
            var facility = await facilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound("Facility not found");

           await facilityRepo.SoftDelete(facility);
            await _unitOfWork.CompleteAsync();

            return Ok("Facility deleted successfully");
        }
    }
}
