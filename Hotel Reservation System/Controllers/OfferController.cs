using AutoMapper;
using Hotel.Core.Dtos.Offer;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.OfferService;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.ViewModels;
using Hotel_Reservation_System.ViewModels.Offer;
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
        //[Authorize(Roles = "Staff")]
        public async Task<ResponseViewModel<OfferViewModel>> CreateOffer(CreateOfferViewModel offerVM)
        {
            var offerDto = _mapper.Map<CreateOfferDto>(offerVM);
            var result = await _offerService.CreateOfferAsync(offerDto);
            var mappedResult = _mapper.Map<OfferViewModel>(result);
            return ResponseViewModel<OfferViewModel>.SuccessResult(mappedResult, "Offer Added Successfully");
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles = "Staff")]
        public async Task<ResponseViewModel<bool>> DeleteOffer(int id)
        {
                await _offerService.DeleteOfferAsync(id);
                return ResponseViewModel<bool>.SuccessResult(true, "Offer deleted successfully");
        }

        [HttpGet]
        //[Authorize(Roles = "Staff")]
        public async Task<ResponseViewModel<IEnumerable<OfferListingDto>>> GetAllOffers()
        {
            var offers = await _offerService.GetActiveOffersAsync();
            return ResponseViewModel<IEnumerable<OfferListingDto>>.SuccessResult(offers,"Offers retrieved successfully" );
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Staff")]
        public async Task<ResponseViewModel<OfferListingDto>> GetOfferByID(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            return ResponseViewModel<OfferListingDto>.SuccessResult(offer, "Offer retrieved successfully");
        }
        [HttpPut]
        //[Authorize(Roles = "Staff")]
        public async Task<ResponseViewModel<bool>> UpdateOffer(UpdateOfferViewModel offerVM)
        {
            var offerDto = _mapper.Map<UpdateOfferDto>(offerVM);
            var result = await _offerService.UpdateOfferAsync(offerDto);
            return ResponseViewModel<bool>.SuccessResult(true, "Offer updated successfully");

        }
    }
}
