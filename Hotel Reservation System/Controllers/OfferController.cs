using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Repository.Services.OfferService;
using Hotel_Reservation_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [ApiController]
    [Route("[action]/[Controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IMapper _mapper;

        public OfferController(IOfferService offerService, IMapper mapper)
        {
            _offerService = offerService;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<OfferViewModel>>> CreateOffer([FromBody] CreateOfferDto offerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel<OfferViewModel>(false, "Invalid offer data", null));
            }

            var result = await _offerService.CreateOfferAsync(offerDto);
            return StatusCode(result.Success ? 201 : 400, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<bool>>> DeleteOffer(int id)
        {
            var result = await _offerService.DeleteOfferAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<IEnumerable<OfferViewModel>>>> GetAllOffers()
        {
            var result = await _offerService.GetAllOffersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<OfferViewModel>>> GetOfferById(int id)
        {
            var result = await _offerService.GetOfferByIdAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<ResponseViewModel<bool>>> UpdateOffer(int id, [FromBody] UpdateOfferDto offerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel<bool>(false, "Invalid offer data", false));
            }

            var result = await _offerService.UpdateOfferAsync(id, offerDto);
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
