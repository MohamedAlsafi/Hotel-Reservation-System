using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Data.Configuration;
using Hotel.Core.Dtos.Offer;
using Hotel_Reservation_System.ViewModels;

namespace Hotel.Repository.Services.OfferService
{
    public interface IOfferService
    {
        Task<OfferDto> CreateOfferAsync(CreateOfferDto offerDto);
        Task<bool> DeleteOfferAsync(int id);
        Task<IEnumerable<OfferListingDto>> GetActiveOffersAsync();
        Task<OfferListingDto> GetOfferByIdAsync(int id);
        Task<OfferDto> UpdateOfferAsync( UpdateOfferDto offerDto);
    }
}
