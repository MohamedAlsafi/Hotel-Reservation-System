using Hotel.Core.Dtos.Offer;
using Hotel.Repository.Services.OfferService;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [ApiController]
    [Route("[action]/[Controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
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
            if (offer == null) return NotFound();
            return Ok(offer);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] CreateOfferDto offerDto)
        {
            var createdOffer = await _offerService.CreateOfferAsync(offerDto);
            return CreatedAtAction(nameof(GetOfferById), new { id = createdOffer.Id }, createdOffer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] CreateOfferDto offerDto)
        {
            var success = await _offerService.UpdateOfferAsync(id, offerDto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var success = await _offerService.DeleteOfferAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
