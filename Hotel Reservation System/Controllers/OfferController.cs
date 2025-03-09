using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Repository.Services.OfferService;
using Hotel_Reservation_System.Error;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [ApiController]
    [Route("[action]/[Controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IMapper _mapper;

        public OfferController(IOfferService offerService , IMapper mapper)
        {
            _offerService = offerService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOffers()
        {
            var offers = await _offerService.GetAllOffersAsync();
            return Ok(offers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null) 
                return NotFound(new ApiResponse(404));

            return Ok(offer);
        }


        [HttpPost]
        public async Task<ActionResult<OfferDto>> CreateOffer([FromBody] CreateOfferDto offerDto)
        {
            var createdOffer = await _offerService.CreateOfferAsync(offerDto);
            return CreatedAtAction(nameof(GetOfferById), new { id = createdOffer }, createdOffer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] UpdateOfferDto offerDto)
        {
            var MappedOffer = _mapper.Map<UpdateOfferDto>(offerDto);
            var Result = await _offerService.UpdateOfferAsync(id, MappedOffer);
            if (!Result.Success)
                return NotFound(new ApiResponse(404));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var response = await _offerService.DeleteOfferAsync(id);
            if (!response.Success) 
                return NotFound(new ApiResponse(404));
            return NoContent();
        }
    }
}
